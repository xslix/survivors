using UnityEngine;
using UnityEngine.Events;

namespace Survivors.Spell
{
    public class Bullet : MonoBehaviour
    {
        public Vector3 velocity;
        public float damage;
        public float recentTime;

        public UnityEvent<Bullet, Collision2D> collisionEvent = new();

        public void OnCollisionEnter2D(Collision2D other)
        {
            collisionEvent?.Invoke(this, other);
        }
    }
}