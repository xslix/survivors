using UnityEngine;
using Zenject;

namespace Survivors
{
    public class SceneDataInstaller : MonoInstaller
    {
        [SerializeField] private SceneData sceneData;
        public override void InstallBindings()
        {
            Container.Bind<SceneData>().FromInstance(sceneData).AsSingle();
        }
    }
}