using UnityEngine;

namespace Survivors.Unit
{
    [CreateAssetMenu(fileName="UnitData", menuName="Settings/UnitData")]
    public class UnitData : ScriptableObject
    {
        public Sprite sprite;
        public float speed;
        public float rotatingSpeed;
        public float damage;
        public float maxHp;
        public float armor;
    }
}
