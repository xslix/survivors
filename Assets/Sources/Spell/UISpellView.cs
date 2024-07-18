using UnityEngine;
using Zenject;
using Image = UnityEngine.UI.Image;

namespace Survivors.Spell
{
    public class UISpellView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [Inject] private ISpellManager _spellManager;

        private void Start()
        {
            _spellManager.SpellChangedEvent.AddListener(UpdateView);
            UpdateView();
        }

        private void UpdateView()
        {
            image.sprite = _spellManager.CurrentSpell.image;
        }

        private void OnDestroy()
        {
            _spellManager.SpellChangedEvent.RemoveListener(UpdateView);
        }
    }
}