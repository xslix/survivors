using UnityEngine;
using Zenject;

namespace Survivors.Spell
{
    [CreateAssetMenu(fileName="LinearSpell", menuName="Settings/Spells/LinearSpell")]
    public class LinearBulletSpell : BulletSpell
    {

        [Inject] private IBulletManager _bulletManager;
        
        public override void Cast(Unit.Unit caster)
        {
            var bullet = _bulletManager.AddBullet(caster.transform.up.normalized * speed, damage, time);
            bullet.transform.position = caster.transform.position;
            bullet.transform.rotation = caster.transform.rotation;
        }
    }
}