using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private static ContactFilter2D TerrainContactFilter2D = new ContactFilter2D()
        {
            layerMask = LayerMask.NameToLayer("Terrain"),
            useTriggers = false,
            useLayerMask = true,
        };

        private Rigidbody2D _rigidBoy;
        private Collider2D[] _colliders;

        public void Awake()
        {
            _rigidBoy = GetComponent<Rigidbody2D>();
            _colliders = GetComponents<Collider2D>();

        }

        public void Start()
        {
            RaycastHit2D[] resultsList = new RaycastHit2D[]{};
            _rigidBoy.Cast(Vector2.down, TerrainContactFilter2D, resultsList, 1f);

            foreach (var raycastHit2D in resultsList)
            {
                
               // raycastHit2D.
            }
        }
    }
}
