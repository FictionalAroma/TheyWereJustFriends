using System;
using System.Collections.Generic;
using System.Linq;
using Behaviors.DataStructs;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private ContactFilter2D _terrainContactFilter2D;

        private Rigidbody2D _rigidBoy;

        [SerializeField] private Transform raycastTestOrigin;
        [SerializeField] private List<CollisionDetectionRay> terrainCollisionRayDirections;

        public void Awake()
        {
            _rigidBoy = GetComponent<Rigidbody2D>();
            _terrainContactFilter2D = new ContactFilter2D()
            {
                layerMask = LayerMask.GetMask("Terrain"),
                useTriggers = false,
                useLayerMask = true,
            };
        }

        public void Start()
        {
            _rigidBoy.velocity += Vector2.right;
        }

        public void FixedUpdate()
        {
            foreach (var raydir in terrainCollisionRayDirections)
            {
                raydir.FixRayDir(_rigidBoy.velocity);
                raydir.DrawRayDebug(raycastTestOrigin.position);
            }
            var changeDir = terrainCollisionRayDirections.Any(ray => ray.DoDetection(raycastTestOrigin.position, _terrainContactFilter2D));
            if (changeDir)
            {
                _rigidBoy.velocity *= -1;
                transform.localScale = new Vector2(Mathf.Sign(_rigidBoy.velocity.x), 1);
                
            }
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Weapon"))
            {
                Destroy(gameObject);
            }
        }
    }
}
