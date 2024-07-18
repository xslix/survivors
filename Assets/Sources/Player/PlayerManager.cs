using Survivors.Spell;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Survivors.Player
{
    public interface IPlayerManager
    {
        public Unit.Unit PlayerUnit { get; }
    }

    public class PlayerManager : IInitializable, ITickable, IPlayerManager
    {
        public Unit.Unit PlayerUnit { get; }

        [Inject] private PlayerControls _playerControls;
        [Inject] private ISpellManager _spellManager;

        public PlayerManager(Unit.Unit playerUnit)
        {
            PlayerUnit = playerUnit;
        }

        public void Initialize()
        {
            _playerControls.BaseMap.NextSpell.performed += NextSpell;
            _playerControls.BaseMap.PreviousSpell.performed += PrevSpell;
            _playerControls.BaseMap.UseSpell.performed += CastSpell;
            PlayerUnit.deathEvent.AddListener(OnPlayerDeath);
        }

        public void Tick()
        {
            var move = _playerControls.BaseMap.Moving.ReadValue<float>();
            var rotate = _playerControls.BaseMap.Rotating.ReadValue<float>();
            if (Mathf.Abs(rotate) > float.Epsilon)
            {
                PlayerUnit.transform.Rotate(0, 0, rotate * Time.deltaTime * PlayerUnit.UnitData.rotatingSpeed);
            }

            if (Mathf.Abs(move) > float.Epsilon)
            {
                PlayerUnit.transform.position +=
                    PlayerUnit.transform.up * (move * Time.deltaTime * PlayerUnit.UnitData.speed);
            }
        }

        private void CastSpell(InputAction.CallbackContext _)
        {
            _spellManager.CastSpell(PlayerUnit);
        }  
        private void PrevSpell(InputAction.CallbackContext _)
        {
            _spellManager.PrevSpell();
        } 
        private void NextSpell(InputAction.CallbackContext _)
        {
            _spellManager.NextSpell();
        }

        private void OnPlayerDeath(Unit.Unit _)
        {
            //Заглушка окончания игры
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif
        }
    }
}