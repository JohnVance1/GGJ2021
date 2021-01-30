// Player Actions
// @author Lingxiao, Assy

using UnityEngine;
using EnemyLogic;

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



    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerAttack), typeof(PlayerSpawn))]
    public class Player : MonoBehaviour
    {
        #region Attrs
        [Min(0)]
        public float moveSpeed = .1f;
        [Min(0)]
        public float jumpForce = 200f;

        public bool debug = true;
        #endregion


        #region Runtime
        [Header("Action Enable")]
        // jump
        public bool enableJump;
        public bool InAir { get; private set; }
        // move
        public bool enableRightMove;
        public bool enableLeftMove;
        public int Facing { get; private set; } // 1 -> right, -1 -> Left
        public float Speed { get; private set; }
        // double jump
        public bool enableDoubleJump;
        private int jumpCount;
        // rush
        public bool enableRush;
        public bool InRush { get; private set; }
        private RushHitCheck rushHitCheck;
        // attack
        public bool enableShoot;
        private PlayerAttack bulletManager;
        // cling
        public bool enableCling;
        private ClingHitCheck clingHitCheck;

        // key press event
        public bool MoveKeyPressed { get; internal set; }
        public bool JumpKeyPressed { get; internal set; }
        public bool ShootKeyPressed { get; internal set; }
        public bool ClingKeyPressed { get; internal set; }

        private Rigidbody2D rb;
        private SpriteRenderer render;
        private PlayerSpawn spawn;
        #endregion


        #region GameCycle
        private void Awake()
        {
            Physics2D.queriesStartInColliders = false;

            Facing = 1;
            Speed = 0;

            rb = GetComponent<Rigidbody2D>();
            render = GetComponent<SpriteRenderer>();

            bulletManager = GetComponent<PlayerAttack>();
            spawn = GetComponent<PlayerSpawn>();

            rushHitCheck = GetComponent<RushHitCheck>();
        }

        private void Start()
        {
            spawn.spawnPoint = transform.position;
        }

        private void Update()
        {
            // shoot if key is continuously pressed
            if (ShootKeyPressed) Shoot();

            // apply velocity, update position
            if (MoveKeyPressed) SetSpeed();
            else Speed = 0;

            // speed -> movement
            Vector3 pos = transform.position;
            Vector3 vel = new Vector3(Speed * Facing, 0, 0);

            // update position
            pos += vel;

            transform.position = pos;
        }
        #endregion


        #region Actions
        // Actions are called on every frame.

        public void FaceTo(PlayerDirection dir)
        {
            if (!enableLeftMove && dir == PlayerDirection.Left) return;
            if (!enableRightMove && dir == PlayerDirection.Right) return;

            Facing = dir.ToFacing();
            render.flipX = Facing < 0;
        }

        public void SetSpeed()
        {
            if (!enableRightMove && Facing > 0) return;
            if (!enableLeftMove && Facing < 0) return;
            Speed = moveSpeed;

            // prevent hit into wall
            if (MoveIsBlocked) Speed = 0;
        }

        public void Jump()
        {
            // basic jump
            if (CanJump)
            {
                if (debug)
                    Debug.Log("Player jump.");

                rb.AddForce(Vector2.up * jumpForce);
                jumpCount++;
                return;
            }

            // double jump
            if (CanDoubleJump)
            {
                if (debug)
                    Debug.Log("Player double jump.");

                Vector2 vel = rb.velocity;
                vel.y = 0;
                rb.velocity = vel;

                rb.AddForce(Vector2.up * jumpForce);
                jumpCount++;
                return;
            }
        }

        public void Shoot()
        {
            if (!enableShoot) return;

            // spawn Bullet prefab
            //   (use a bullet manager to handle all bullet movements & collisions,
            //   instead of updating independent gameObject, for efficiency.)
            Vector3 pos = transform.position;
            Vector2 dir = Facing > 0 ? Vector2.right : Vector2.left;

            bulletManager.ShootStart(pos, dir);
        }

        public void Rush()
        {
            if (!enableRush) return;
            // be careful with collision detection
            foreach (var HitEnemyObject in rushHitCheck.GetRushAreainEnemyObjects())
            {
                var enemybasic = HitEnemyObject.GetComponent<EnemyBasic>();
                if (enemybasic != null)
                {
                    // todo enemy damage
                    Debug.Log("Player hit enemy: " + enemybasic.name);
                }
            }
        }

        public void Cling()
        {
            if (!enableCling) return;
            // attach to wall, cannot move, can jump
            // todo Lingxiao
        }

        public void Spwan()
        {
            // player dies and respawn
        }
        #endregion


        #region CollisionCheck
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Block"))
            {
                // if hit ground from upside, reset
                if (HitTop(collision))
                {
                    if (debug)
                        Debug.Log("Player hit ground.");

                    InAir = false;
                    jumpCount = 0;
                    Speed = 0;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Block"))
            {
                if (HitTop(collision))
                {
                    if (debug)
                        Debug.Log("Player leave ground.");

                }
                InAir = true;

            }
        }

        //アイテム関連でトリガーを扱っているためここに書いています。
        //I'm writing this here because I'm dealing with triggers in an item-related way.
        //Author Shion
        private void OnTriggerEnter2D(Collider2D trigger) {
            var obj = trigger.gameObject;
            if (obj.CompareTag("Item")) {
                var ITEMS = obj.GetComponent<ItemObjects>();
                if (ITEMS == null) return;
                switch (ITEMS.GetItemType()) {
                    case ItemType.Item_Jump:
                        enableJump = true;
                        break;
                    case ItemType.Item_Climb:
                        enableCling = true;
                        break;
                    case ItemType.Item_Left:
                        enableLeftMove = true;
                        break;
                    case ItemType.Item_Rush:
                        enableRush = true;
                        break;
                    case ItemType.Item_DoubleJump:
                        enableDoubleJump = true;
                        break;
                    case ItemType.Item_SlotAdd:
                        Debug.Log("Player add slot.");
                        break;
                }
                Destroy(obj);
            }
        }
        #endregion


        #region Utils
        private bool CanJump { get { return enableJump && jumpCount == 0 && !InAir; } }
        private bool CanDoubleJump { get { return enableDoubleJump && jumpCount == 1; } }
        private bool MoveIsBlocked
        {
            get
            {
                Vector2 dir = Facing > 0 ? Vector2.right : Vector2.left;
                Vector3 p0 = transform.position;
                Vector3 p1 = new Vector3(p0.x, p0.y - .4f, 0);
                Vector3 p2 = new Vector3(p0.x, p0.y + .4f, 0);
                RaycastHit2D hit1 = Physics2D.Raycast(p1, dir, moveSpeed * 2);
                RaycastHit2D hit2 = Physics2D.Raycast(p2, dir, moveSpeed * 2);
                return hit1.collider != null || hit2.collider != null;
            }
        }

        private bool HitTop(Collision2D collision)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y - transform.position.y <= -.5) return true;
            }
            return false;
        }

        internal void PlayerAngleChangeLeft()
        {
            if (System.Math.Abs(transform.localRotation.y - 180) > .001f)
            {
                var ro = transform.localRotation;
                ro.y = 180;
                transform.localRotation = ro;
            }
        }
        internal void PlayerAngleChangeRight()
        {
            if (System.Math.Abs(transform.localRotation.y) > .001f)
            {
                var ro = transform.localRotation;
                ro.y = 0;
                transform.localRotation = ro;
            }
        }
        #endregion
    }
}
