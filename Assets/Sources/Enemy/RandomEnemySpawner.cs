using Survivors.Player;
using UnityEngine;
using Zenject;

namespace Survivors.Enemy
{
    public class RandomEnemySpawner : IEnemySpawner
    {
        private const float RANGE_MODIFIER = 1.2f;
        [Inject] private IPlayerManager _playerManager;
        [Inject] private SceneData _sceneData;
        
       
        public Unit.Unit Spawn(IObjectPool<Unit.Unit> pool)
        {
            var playerPos = _playerManager.PlayerUnit.transform.position;
            var cornerPos = _sceneData.mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _sceneData.mainCamera.nearClipPlane));
            var distance = Vector3.Distance(playerPos, cornerPos) * RANGE_MODIFIER;
            var enemy = pool.Get();
            enemy.gameObject.tag = Tags.Enemy;
            enemy.gameObject.layer = LayerMask.NameToLayer(Tags.Enemy);
            var unitsData = _sceneData.generalSettings.enemies;
            enemy.Init(unitsData[Random.Range(0, unitsData.Count)]);
            Vector3 position;
            do
            {
                var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                position = playerPos + new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
                position.z = 0;
            } while (!_sceneData.map.bounds.Contains(position));

            enemy.transform.position = position;
            return enemy;
        }
    }
}