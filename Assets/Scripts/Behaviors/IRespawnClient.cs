using Environment;
using UnityEngine;

namespace Behaviors
{
    
    public interface IRespawnClient
    {
        public bool InRespawnProcess { get; set; }
        public bool CanActivateSpawner { get; set; }
        [SerializeField] public float RespawnTimer { get; set; }
        public void RespawnActivated(RespawnPoint currentPoint);
    }
}