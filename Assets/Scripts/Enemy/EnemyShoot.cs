using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerLogic;

namespace EnemyLogic
{
    public class EnemyShoot : EnemyBasic
    {
        EnemyStates enemyShootState = EnemyStates.Idle;
        private Rigidbody2D rb;
        private bool jumping;
        private int jumpPower;
        private float jumpDelay;
        private float timeDelay;
        private EnemyAttack bulletManager;


        public override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
            jumping = true;
            jumpPower = 10;
            jumpDelay = 2.0f;
            timeDelay = 0.0f;
            moveSpeed = 0.01f;
            bulletManager = GetComponent<EnemyAttack>();

        }

        public override void EnemyAttack()
        {
            FindPlayerDirection();
            ShootMove();
            Shoot();
        }

        public override void EnemyIdle()
        {
            ShootMove();
        }

        public void ShootMove()
        {
            Vector3 pos = transform.position;
            Vector3 vel = new Vector3(moveSpeed * direction, 0, 0);
            pos += vel;

            transform.position = pos;

            

        }

        public void Shoot()
        {
            int jumpAngle = Random.Range(30, 75);
            Vector2 jumpVector = Rotate(Vector2.up, direction * jumpAngle * Mathf.Deg2Rad);

            if (!jumping && CanJump() && (player.GetComponent<Player>().InAir))
            {
                //yield return new WaitForSeconds(5.0f);
                jumpPower = 200;
                rb.AddForce(jumpVector * jumpPower);

                timeDelay = Time.time + jumpDelay;

            }

            Vector3 pos = transform.position;
            Vector2 dir = direction > 0 ? Vector2.right : Vector2.left;

            bulletManager.ShootStart(pos, dir);
        }

        public bool CanJump()
        {
            return (Time.time > timeDelay);
        }

        public static Vector2 Rotate(Vector2 v, float radians)
        {
            var ca = Mathf.Cos(radians);
            var sa = Mathf.Sin(radians);
            return new Vector2(ca * v.x + sa * v.y, sa * v.x + ca * v.y);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            if (collision.gameObject.CompareTag("Block"))
            {
                jumping = false;

            }

        }

        protected override void OnCollisionExit2D(Collision2D collision)
        {
            base.OnCollisionExit2D(collision);
            if (collision.gameObject.CompareTag("Block"))
            {
                jumping = true;

            }
        }

    }

}
