using Zenject;

namespace Survivors.Player
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var controls = new PlayerControls();
            controls.Enable();
            Container.Bind<PlayerControls>().FromInstance(controls).AsSingle();
        }
    }
}