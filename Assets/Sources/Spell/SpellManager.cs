using System.Collections.Generic;
using UnityEngine.Events;

namespace Survivors.Spell
{
    public interface ISpellManager
    {
        public void NextSpell();
        public void PrevSpell();
        public void CastSpell(Unit.Unit caster);
        
        public SpellBase CurrentSpell { get; }

        public UnityEvent SpellChangedEvent { get; }
    }
 // В этой реализации я предполагаю, что у противников не может быть спеллов, но легко расширить, немного изменив логику слоев
    public class SpellManager : ISpellManager
    {
        private List<SpellBase> spells;
        private int _currentSpell = 0;

        public SpellManager(List<SpellBase> spells)
        {
            this.spells = spells;
        }

        public void NextSpell()
        {
            _currentSpell++;
            if (_currentSpell == spells.Count)
            {
                _currentSpell = 0;
            }
            SpellChangedEvent?.Invoke();
        }

        public void PrevSpell()
        {
            _currentSpell--;
            if (_currentSpell < 0)
            {
                _currentSpell = spells.Count-1;
            }
            SpellChangedEvent?.Invoke();
        }

        public void CastSpell(Unit.Unit caster)
        {
            spells[_currentSpell].Cast(caster);
        }

        public SpellBase CurrentSpell => spells[_currentSpell];

        public UnityEvent SpellChangedEvent { get; private set; } = new();
    }
}