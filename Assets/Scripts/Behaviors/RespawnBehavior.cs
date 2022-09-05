using Environment;
using UnityEngine;

namespace Behaviors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class RespawnBehavior : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.GameObject unityRespawnObject; // works

        private IRespawnClient _respawnClient = null;

        private RespawnPoint _activePoint;

        private IRespawnClient RespawnClient
        {
            get
            {
                if (_respawnClient == null)
                {
                    _respawnClient = unityRespawnObject.GetComponent<IRespawnClient>();
                }

                return _respawnClient;
            }
        }

        private void Start()
        {
            //var filter = new ContactFilter2D()
            //{
            //    layerMask = LayerMask.GetMask("RespawnInteraction"),
            //    useTriggers = true,
            //    useLayerMask = true,
            //};

            //var raycastHit2Ds = new List<RaycastHit2D>();
            //var detectionRadius = 1.0f;
            //while (Physics2D.CircleCast(transform.position, detectionRadius, Vector2.zero, filter, raycastHit2Ds) == 0)
            //{
            //    detectionRadius += 0.5f;
            //}

            //if (raycastHit2Ds.Count == 1)
            //{
            //    _activePoint = GetSpawnPointFromRaycast(raycastHit2Ds[0]);
            //}
        }

        private RespawnPoint GetSpawnPointFromRaycast(RaycastHit2D raycastHit2D) => raycastHit2D.transform.gameObject.GetComponent<RespawnPoint>();

        public void OnTriggerEnter2D(Collider2D other) => CheckActivateSpawner(other);
        public void OnTriggerStay2D(Collider2D other) => CheckActivateSpawner(other);

        private void CheckActivateSpawner(Component other)
        {
            //again sanity check
            if (other.TryGetComponent<RespawnPoint>(out var newPoint) && RespawnClient.CanActivateSpawner)
            {
                if (_activePoint != null)
                {
                    _activePoint.SetActive(false);
                }

                _activePoint = newPoint;
                _activePoint.SetActive(true);
            }

        }

        public void DoRespawn()
        {
            _respawnClient.RespawnActivated(_activePoint);
        }
    }
}