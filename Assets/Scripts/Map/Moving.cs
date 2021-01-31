using UnityEngine;

namespace MapLogic
{
    public class Moving : MonoBehaviour
    {
        public Vector3[] waypoints;
        [Range(0, .1f)]
        public float speed = .001f;
        public bool debug = true;

        public Vector2 vel;

        // current waypoint index
        private int idx = 0;
        // current move progress
        private float t;

        private void Awake()
        {
            if (waypoints == null) return;
            for (int i = 0; i < waypoints.Length; i++)
                waypoints[i].z = 0;

            if (waypoints.Length >= 0)
                transform.position = waypoints[0];
        }

        private void FixedUpdate()
        {
            if (waypoints == null || waypoints.Length <= 1) return;

            // loop between waypoints
            Vector3 target = waypoints[idx];
            Vector3 pos = transform.position;

            // if is close
            if (Vector3.Distance(target, pos) < .01f)
            {
                transform.position = target;
                idx = (idx + 1) % waypoints.Length;
                t = 0;
            }
            // smooth move
            else
            {
                t = (t + speed) % 1f;
                transform.position = Vector3.Lerp(pos, target, t);

                vel = transform.position - pos;
            }
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;
            // debug waypoints
            if (waypoints == null) return;
            for (int i = 0; i < waypoints.Length; i++)
                Gizmos.DrawSphere(waypoints[i], .1f);
        }
    }
}
