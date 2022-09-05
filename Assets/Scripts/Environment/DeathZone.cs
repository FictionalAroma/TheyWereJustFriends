using System;
using Behaviors;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<RespawnBehavior>(out var respawn))
            {
                respawn.DoRespawn();
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
    }
}
