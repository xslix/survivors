using UnityEngine;

namespace Survivors.Spell
{
    public abstract class SpellBase : ScriptableObject
    {
        public Sprite image;
        public abstract void Cast(Unit.Unit caster);
    }

    public abstract class BulletSpell : SpellBase
    {
        public Sprite bulletImage;
        public float damage;
        public float speed;
        public float time;
    }
}