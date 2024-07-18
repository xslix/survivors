using UnityEngine;
using Zenject;

namespace Survivors.Spell
{
    [CreateAssetMenu(fileName="CircleSpell", menuName="Settings/Spells/CircleSpell")]

    public class CircleBulletSpell: BulletSpell
    {

        [Inject] private IBulletManager _bulletManager;
        
        public override void Cast(Unit.Unit caster)
        {
            //Я понимаю что все эти три спелла можно свести к одному частному случаю с настройкой количества пуль, но тип как пример разных реализаций
            var rotate = Quaternion.Euler(0,0,0);
            for (int i = 0; i < 12; ++i)
            {
                var bullet = _bulletManager.AddBullet(rotate * caster.transform.up.normalized * speed, damage, time);
                bullet.transform.position = caster.transform.position;
                bullet.transform.rotation = rotate * caster.transform.rotation;
                rotate *= Quaternion.Euler(0, 0, 30);
            }
        }
    }
}