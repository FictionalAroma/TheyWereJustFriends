using UnityEngine;

namespace Behaviors
{
    [RequireComponent(typeof(Collider2D))]
    public class KnockbackTrigger : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent<IKnockbackReaction>(out var knockback))
            {
                knockback.Knockback(transform);
            }
        }
    }
}
