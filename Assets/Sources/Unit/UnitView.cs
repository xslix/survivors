using UnityEngine;

namespace Survivors.Unit
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}