using System.Collections;
using UnityEngine;

namespace Behaviors
{
    public interface IKnockbackReaction
    {
        bool KnockbackActive { get; set; }
        float KnockbackForce { get; set; }

        void Knockback(Transform trigger);

        IEnumerator RunKnockbackEffect();

    }
}
