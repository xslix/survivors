using UnityEngine;
using UnityEngine.Events;

namespace Survivors.Unit
{ 
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitView unitView;
        public UnitData UnitData { get; private set; }
        public float Hp { get; private set; }
        public UnityEvent<Unit> deathEvent = new ();
        public UnityEvent<Unit, Collision2D> collisionEvent = new();
        public UnityEvent takeDamageEvent = new();
        
        public void TakeDamage(float damage)
        {
            Hp = Mathf.Clamp(Hp - damage * (1 - UnitData.armor), 0, UnitData.maxHp);
            takeDamageEvent?.Invoke();
            if (Hp < float.Epsilon)
            {
                deathEvent?.Invoke(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            collisionEvent?.Invoke(this, other);
        }
        

        public void Init(UnitData data)
        {
            UnitData = data;
            Hp = data.maxHp;
            unitView.SetSprite(data.sprite);
        }
    }
}