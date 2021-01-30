// @author Assy
// attached to a bullet and check bullet hit.
using UnityEngine;

namespace PlayerLogic
{
    public class ShootHitCheck : MonoBehaviour
    {
        public bool HitFlag = false;

        // todo this should check what it hits: player? enemy? wall?
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (/*!collision.gameObject.CompareTag("Player") &&*/ 
                !collision.gameObject.CompareTag("PlayerArea")  
                /*&& !collision.gameObject.CompareTag("Bullet")*/)
            {
                HitFlag = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (/*!collision.gameObject.CompareTag("Player") && */ 
                !collision.gameObject.CompareTag("PlayerArea") 
                /*&& !collision.gameObject.CompareTag("Bullet") */)
            {
                HitFlag = true;
            }
        }

    }
}
