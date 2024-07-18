using System.Collections.Generic;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Survivors.Spell
{
    public interface IBulletManager
    { public Bullet AddBullet(Vector3 velocity, float damage, float recentTime);
    }
    public class BulletManager : ITickable,  IInitializable, IBulletManager
    {
        private readonly Bullet _bulletPrefab;

        public BulletManager(Bullet bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        private ObjectPool<Bullet> Pool;
        
        public Bullet AddBullet(Vector3 velocity, float damage, float recentTime)
        {
            var bullet = Pool.Get();
            bullet.damage = damage;
            bullet.velocity = velocity;
            bullet.recentTime = recentTime;
            bullet.gameObject.layer = LayerMask.NameToLayer(Tags.Bullet);
            bullet.gameObject.tag = Tags.Bullet;
            bullets.Add(bullet);
            bullet.collisionEvent.AddListener(OnBulletCollision);
            return bullet;
        }

        private List<Bullet> bullets = new();

        public void Initialize()
        {
            Pool = new ObjectPool<Bullet>(_bulletPrefab);
        }

        public void Tick()
        {
            //Все же, имхо ECS подход тут бы выйграл много производительности
            for (int i=0; i < bullets.Count; ++i)
            {
                bullets[i].transform.position += bullets[i].velocity * Time.deltaTime;
                bullets[i].recentTime -= Time.deltaTime;
                if (bullets[i].recentTime < 0)
                {
                    RemoveBullet(bullets[i]);
                    bullets.RemoveAt(i);
                    --i;
                }
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Tags.Enemy))
            {
                var unit = collision.gameObject.GetComponent<Unit.Unit>();
                unit.TakeDamage(bullet.damage);
            }

            bullets.Remove(bullet);
            RemoveBullet(bullet);
            
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.velocity = Vector3.zero;
            bullet.recentTime = 0;
            bullet.damage = 0;
            bullet.collisionEvent.RemoveAllListeners();
            bullet.gameObject.SetActive(false);
            Pool.Release(bullet);
        }

    }
}