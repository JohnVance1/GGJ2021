using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class PlayerSpawn : MonoBehaviour
    {
        // store where player re-spawns
        public Vector3 spawnPoint;
        public bool debug;

        // debug the spawn point
        private void OnDrawGizmos()
        {
            if (debug)
                Gizmos.DrawCube(spawnPoint, Vector3.one * .1f);
        }
    }
}
