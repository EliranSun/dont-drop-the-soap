using UnityEngine;

namespace Ryan.Scripts {
    [RequireComponent(typeof(Collider2D))]
    public class HeadColliderController : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private GameObject _head;
        private int _hitCount;

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Death by concrete")) {
                if (_hitCount == 3) {
                    EventManager.Instance.Publish(GameEvents.HitByConcrete);
                    return;
                }

                _hitCount++;
                var damage = 1 - _hitCount * 0.33f;
                spriteRenderer.color = new Color(1f, damage, damage);
            }
        }
    }
}