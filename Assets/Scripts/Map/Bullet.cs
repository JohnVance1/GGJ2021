using UnityEngine;

namespace MapLogic
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        Transform trans;
        public float angle;

        private void Awake()
        {
            trans = transform;
        }

        void Update()
        {
            Vector3 pos = trans.position;
            pos.x -= speed * Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime;
            pos.y -= speed * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime;
            trans.position = pos;
        }

        // fixme if it doesn't hit anything, it never destroys
        // fixme give it a lifetime
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}