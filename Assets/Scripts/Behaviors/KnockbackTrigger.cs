using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviors
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
