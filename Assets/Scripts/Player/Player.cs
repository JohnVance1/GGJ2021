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
            //return dir switch
            //{
            //    PlayerDirection.Left => -1,
            //    PlayerDirection.Right => 1,
            //    _ => 0,
            //};
            switch (dir)
            {
                case PlayerDirection.Left: return -1;
                case PlayerDirection.Right: return 1;
                default: return 0;
            }
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

        // Freeze when player adjust key slots on UI
        public bool freeze;
        public bool debug = true;

        [Min(0)]
        public int MaxSlots = 1;
        public int SlotCount { get; private set; }

        //Shion--------------------------
        public int InitHP = 3;//InitialHP
        int nowHP = 0;

        //-------------------------------
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
        private PlayerRush playerRush;
        public float RushTime;
        // attack
        public bool enableShoot;
        private PlayerAttack bulletManager;
        // cling
        public bool enableCling;

        // key press event
        public bool MoveKeyPressed { get; internal set; }
        public bool JumpKeyPressed { get; internal set; }
        public bool ShootKeyPressed { get; internal set; }
        public bool ClingKeyPressed { get; internal set; }
        public bool RushKeyPressed { get; internal set; }

        private Rigidbody2D rb;
        private SpriteRenderer render;
        private PlayerSpawn spawn;
        public InputListener Input { get; private set; }

        //shion-------------------------
        public bool isDamaged { get; private set; }
        //------------------------------
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
            Input = GetComponent<InputListener>();

            nowHP = InitHP;
            rushHitCheck = GetComponentInChildren<RushHitCheck>();

            playerRush = GetComponent<PlayerRush>();

            UpdateSlotCount();
        }

        private void Start()
        {
            spawn.spawnPoint = transform.position;
        }

        private void Update()
        {
            if (freeze) return;

            if (JumpKeyPressed) Jump();

            // pressing the Cling key.
            if (ClingKeyPressed) { Cling(); return; }
            else rb.isKinematic = false;

            // shoot if key is continuously pressed
            if (ShootKeyPressed) Shoot();

            // apply velocity, update position
            if (MoveKeyPressed) SetSpeed();
            else Speed = 0;
            
            // rush pressed
            if (RushKeyPressed) Rush();

            // speed -> movement
            Vector3 pos = transform.position;
            Vector3 vel = new Vector3(Speed * Facing, 0, 0);

            // update position
            pos += vel;

            transform.position = pos;

            Vector2 m_Vec = new Vector2(Facing, 0.0f);

            //Rush Offset Change
            rushHitCheck.ChangeOffset(m_Vec);
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
            if (MoveIsBlocked)
            {
                if (debug)
                    Debug.Log("Player is blocked.");
                Speed = 0;
            }
        }

        public void Jump()
        {
            // basic jump
            if (CanJump)
            {
                if (debug)
                    Debug.Log("Player jump.");

                // cancel cling
                if (rb.isKinematic)
                {
                    rb.isKinematic = false;
                    ClingKeyPressed = false;
                    // set speed to back
                    rb.AddForce(new Vector2(-Facing, 0) * jumpForce * .05f);
                }
                // jump
                rb.AddForce(Vector2.up * jumpForce);
                jumpCount++;
                //jumpFlagOff
                JumpKeyPressed = false;
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

                // jump
                rb.AddForce(Vector2.up * jumpForce);
                jumpCount++;
                //jumpFlagOff
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
            //no use?   
            //if(!InRush) return;

            RushKeyPressed = false;

            playerRush.PushMoveStart(Facing);

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
            // use ray cast to find the wall to cling, more reliable.
            // if is blocked, it means player is next to a wall.
            if (!MoveIsBlocked) return;

            // attach to wall, cannot move, can jump
            if (!rb.isKinematic)
            {
                if (debug)
                    Debug.Log("Player cling to wall.");

                rb.isKinematic = true;
                InAir = false;
                jumpCount = 0;

                //rb stop
                Vector2 _vel = rb.velocity;
                _vel.y = 0;
                rb.velocity = _vel;
            }
        }

        public void SaveSpawnPoint()
        {
            Debug.Log("Player save spawn point at: " + spawn.spawnPoint);
            spawn.spawnPoint = transform.position;
        }

        public void Spawn()
        {
            Debug.Log("Player respawn at: " + spawn.spawnPoint);
            // player dies and respawn
            transform.position = spawn.spawnPoint;

            render.color = Color.white;
            nowHP = InitHP;
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
            } else if (collision.gameObject.CompareTag("Enemy")) {
                if (debug) Debug.Log("Player hit Enemy.");
                nowHP--;
                isDamaged = true;
                Color c = Color.white;
                c.a = 0.5f;
                render.color = c;
                Invoke("waitHit", 1f);
                if (nowHP <= 0) Spawn();
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
                    InAir = true;
                }
            }
        }

        //アイテム関連でトリガーを扱っているためここに書いています。
        //I'm writing this here because I'm dealing with triggers in an item-related way.
        //Author Shion
        private void OnTriggerEnter2D(Collider2D trigger)
        {
            var obj = trigger.gameObject;
            if (obj.CompareTag("Item"))
            {
                var item = obj.GetComponent<ItemObjects>();
                if (item == null) return;
                if (SlotCount >= MaxSlots && item.GetItemType() != ItemType.Item_SlotAdd)
                {
                    Debug.Log("Player slots are full. Can not pick up new item.");
                    return;
                }

                switch (item.GetItemType())
                {
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
                        MaxSlots++;
                        break;
                }
                Destroy(obj);
                UpdateSlotCount();

                if (debug)
                    Debug.Log("Player & NPC are freezed when adjust key slots.");
                Freeze();
                EnemyBasic.FreezeEvent?.Invoke();

                if (debug)
                    Debug.Log("Bring up slot UI.");
                UI.SlotUI.PickUpEvent?.Invoke(this);
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
                float offset = contact.point.y - transform.position.y;
                if (offset <= -.5f && rb.velocity.y <= 0) return true;
            }
            return false;
        }

        public void Freeze()
        {
            freeze = true;
            Input.freeze = true;
        }

        public void Resume()
        {
            freeze = false;
            Input.freeze = false;
        }

        public void UpdateSlotCount()
        {
            SlotCount = 0;
            if (enableRightMove) SlotCount ++;
            if (enableLeftMove) SlotCount++;
            if (enableJump || enableDoubleJump) SlotCount++;
            if (enableRush) SlotCount++;
            if (enableShoot) SlotCount++;
            if (enableCling) SlotCount++;
        }
        #endregion
    }
}
