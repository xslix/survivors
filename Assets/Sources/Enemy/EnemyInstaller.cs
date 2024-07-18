using Zenject;

namespace Survivors.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var spawner = Container.Instantiate<RandomEnemySpawner>();
            Container.Bind<IEnemySpawner>().FromInstance(spawner).AsSingle();
            Container.BindInterfacesTo<EnemyManager>().AsSingle();
        }
    }
}