using UnityEngine;

namespace PlayerLogic
{
    public class ClingHitCheck : MonoBehaviour
    {
        public bool ClingFlag = false;

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
