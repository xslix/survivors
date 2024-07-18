using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Survivors.Player
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image value;

        [Inject] private IPlayerManager _playerManager;
        //Этот класс представляет конкретный UI бар, в боевом проекте я был делал общий класс для всех хп-баров.

        private void Start()
        {
            _playerManager.PlayerUnit.takeDamageEvent.AddListener(RecalculateBar);
        }

        private void RecalculateBar()
        {
            var percent = _playerManager.PlayerUnit.Hp / _playerManager.PlayerUnit.UnitData.maxHp;
            value.rectTransform.sizeDelta =  new Vector2(background.rectTransform.rect.width * percent, value.rectTransform.sizeDelta.y);
        }

        private void OnDestroy()
        {
            _playerManager.PlayerUnit.takeDamageEvent.RemoveListener(RecalculateBar);
        }
    }
}