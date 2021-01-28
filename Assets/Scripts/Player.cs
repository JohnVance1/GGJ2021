// Player Actions
// @author Lingxiao, Assy

using UnityEngine;

namespace PlayerLogic
{
    #region
    public enum PlayerDirection
    {
        Left, Right
    }

    public static class PlayerDirectionExtension
    {
        public static int ToFacing(this PlayerDirection dir)
        {
            return dir switch
            {
                PlayerDirection.Left => -1,
                PlayerDirection.Right => 1,
                _ => 0,
            };
        }
    }
    #endregion



    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region Attrs
        [Min(0)]
        public float moveSpeed = 1f;
        [Min(0)]
        public float jumpForce = 1f;

        public bool debug = true;
        #endregion


        #region Runtime
        // jump
        public bool enableJump { get; private set; }
        public bool InAir { get; private set; }
        // move
        public int Facing { get; private set; } // 1 -> right, -1 -> Left
        public float Speed { get; private set; }
        // double jump
        public bool enableDoubleJump;
        private int jumpCount;
        // rush
        public bool InRush { get; private set; }

        // key press event
        public bool MoveKeyPressed { get; internal set; }
        public bool JumpKeyPressed { get; internal set; }

        private Rigidbody2D rb;
        #endregion


        #region GameCycle
        private void Awake()
        {
            Facing = 1;
            Speed = 0;

            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // apply velocity,
            // update position,

            if (MoveKeyPressed) Move();
            else Speed = 0;

            Vector3 pos = transform.position;
            Vector3 vel = new Vector3(Speed * Facing, 0, 0);
            pos += vel;

            transform.position = pos;
        }
        #endregion


        #region Actions
        // Actions are called on every frame.

        public void FaceTo(PlayerDirection dir)
        {
            Facing = dir.ToFacing();
        }

        public void Move()
        {
            Speed = moveSpeed;
        }

        public void Jump()
        {
            // basic jump
            if (jumpCount == 0 && enableJump && !InAir)
            {
                if (debug)
                    Debug.Log("Player jump.");

                rb.AddForce(Vector2.up * jumpForce);
                jumpCount++;
            }

            // double jump
            if (jumpCount == 1 && enableDoubleJump)
            {
                if (debug)
                    Debug.Log("Player double jump.");

                rb.AddForce(Vector2.up * jumpForce);
                jumpCount++;
            }
        }

        public void Shoot()
        {
            // spawn Bullet prefab
            //   (use a bullet manager to handle all bullet movements & collisions,
            //   instead of updating independet gameObject, for efficiency.)
        }

        public void Rush()
        {
            // be careful with collision detection
        }

        public void Cling()
        {
            // attach to wall, cannot move, can jump
        }
        #endregion


        private void OnCollisionEnter2D(Collision2D collision)
        {
            // todo if hit ground from upside, reset
            if (collision.gameObject.CompareTag("Block"))
            {
                if (debug)
                    Debug.Log("Player hit ground.");

                InAir = false;
                jumpCount = 0;
                Speed = 0;
            }

            // todo if hit block from left / right, set speed to 0
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Block"))
            {
                if (debug)
                    Debug.Log("Player leave ground.");

                InAir = true;
            }
        }
    }
}
