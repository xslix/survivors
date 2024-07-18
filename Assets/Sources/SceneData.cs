using Cinemachine;
using UnityEngine;

namespace Survivors
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer map;
        [SerializeField] public CinemachineVirtualCamera virtualCamera;
        [SerializeField] public Camera mainCamera;
        [SerializeField] public GeneralSettings generalSettings;
        [SerializeField] public Survivors.Unit.Unit unitPrefab;
    }
}