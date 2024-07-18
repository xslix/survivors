namespace Survivors.Enemy
{
    public interface IEnemySpawner
    {
        public Unit.Unit Spawn(IObjectPool<Unit.Unit> pool);
    }
}