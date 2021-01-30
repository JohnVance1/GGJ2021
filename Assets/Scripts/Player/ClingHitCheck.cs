using UnityEngine;

namespace PlayerLogic
{
    public class ClingHitCheck : MonoBehaviour
    {
        public bool ClingFlag = false;

        private BoxCollider2D c_bcoll;

        private void Awake()
        {
            c_bcoll = GetComponent<BoxCollider2D>();
        }

        public void ChangeOffset(Vector2 _moveVec)
        {
            if (c_bcoll.offset.x < 0 && _moveVec.x > 0)
            {
                c_bcoll.offset = -c_bcoll.offset;
            }
            else if (c_bcoll.offset.x > 0 && _moveVec.x < 0)
            {
                c_bcoll.offset = -c_bcoll.offset;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Block"))
            {
                ClingFlag = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Block"))
            {
                ClingFlag = false;
            }
        }
    }
}
