using System;
using UnityEngine;

namespace Behaviors.DataStructs
{
    [Serializable]
    public class CollisionDetectionRay
    {
        [SerializeField] private Vector2 rayDirection;
        [SerializeField] private float rayDistance;
        
        [SerializeField, Tooltip("True for Fires on Hit, False for on No Hit")] private bool fireOnHit;

        [SerializeField] private Color debugColor = Color.green;

        private Vector2 _currentRay;

        public void DrawRayDebug(Vector2 startpoint)
        {
            Debug.DrawRay(startpoint, rayDirection, debugColor, rayDistance);
        }

        public bool DoDetection(Vector2 startPoint, ContactFilter2D filter)
        {
            var results = new RaycastHit2D[1];
            var resultNumber = Physics2D.Raycast(startPoint, _currentRay, filter, results, rayDistance);

            return fireOnHit ? resultNumber > 0 : resultNumber == 0;
        }

        public void FixRayDir(Vector2 movementDir)
        {
            _currentRay = new Vector2(rayDirection.x * Mathf.Sign(movementDir.x), rayDirection.y * Mathf.Sign(movementDir.y));
        }
    }
}
