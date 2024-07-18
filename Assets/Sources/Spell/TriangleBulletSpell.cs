using UnityEngine;
using Zenject;

namespace Survivors.Spell
{
    [CreateAssetMenu(fileName="TriangleSpell", menuName="Settings/Spells/TriangleSpell")]
    public class TriangleBulletSpell: BulletSpell
    {

        [Inject] private IBulletManager _bulletManager;
        
        public override void Cast(Unit.Unit caster)
        {
            var rotate = Quaternion.Euler(0,0,-30);
            for (int i = 0; i < 3; ++i)
            {
                var bullet = _bulletManager.AddBullet(rotate * caster.transform.up.normalized * speed, damage, time);
                bullet.transform.position = caster.transform.position;
                bullet.transform.rotation = rotate * caster.transform.rotation;
                rotate *= Quaternion.Euler(0, 0, 30);
            }

        }
    }
}