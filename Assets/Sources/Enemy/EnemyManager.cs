using System.Collections.Generic;
using Survivors.Player;
using UnityEngine;
using Zenject;

namespace Survivors.Enemy
{
    public interface IEnemyManager
    {
    }

    public class EnemyManager : ITickable, IInitializable, IEnemyManager
    {
        [Inject] private IEnemySpawner _enemySpawner;
        [Inject] private SceneData _sceneData;
        [Inject] private IPlayerManager _playerManager;

        private ObjectPool<Unit.Unit> _pool;
        private List<Unit.Unit> enemies = new();

        public void Initialize()
        {
            _pool = new ObjectPool<Unit.Unit>(_sceneData.unitPrefab);
            for (int i = 0; i < _sceneData.generalSettings.enemiesCount; ++i)
            {
                AddEnemy();
            }
        }

        public void Tick()
        {
            var pos = _playerManager.PlayerUnit.transform.position;
            foreach (var unit in enemies)
            {
                var direction = (pos - unit.transform.position).normalized;
                unit.transform.Translate(direction * unit.UnitData.speed * Time.deltaTime);
            }
        }

        private void AddEnemy()
        {
            var enemy = _enemySpawner.Spawn(_pool);
            enemies.Add(enemy);
            enemy.collisionEvent.AddListener(OnEnemyCollision);
            enemy.deathEvent.AddListener(OnEnemyDeath);
        }

        private void OnEnemyCollision(Unit.Unit enemy, Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
            {
                //Наверняка в боевом проекте логика будет более сложная, но тут сделал по-простому, и плеер не получает урона от длительного контакта
                _playerManager.PlayerUnit.TakeDamage(enemy.UnitData.damage);
            }
        }

        private void OnEnemyDeath(Unit.Unit unit)
        {
            _pool.Release(unit);
            unit.collisionEvent.RemoveAllListeners();
            unit.deathEvent.RemoveAllListeners();
            enemies.Remove(unit);
            AddEnemy();
            //Тут я понял, что для условия, что противников всего 10, пул не нужен, но оставил его на потенциал расширения логики спавна
        }
    }
}