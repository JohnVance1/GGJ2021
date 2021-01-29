using UnityEngine;

namespace EnemyLogic
{
    /// <summary>
    /// Author: John Vance
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBasic : MonoBehaviour
    {
        #region Variables
        private SpriteRenderer debugSprite;
        [SerializeField]
        protected GameObject player;
        protected GameObject thisEnemyObj;
        protected int direction;
        [SerializeField]
        private float moveSpeed;
        #endregion


        public enum EnemyStates
        {
            Idle, Attack
        }

        EnemyStates enemyState = EnemyStates.Idle;

        public virtual void Start()
        {
            debugSprite = GetComponent<SpriteRenderer>();
            direction = 1;
            RandomDirection();
            moveSpeed = 0.01f;

        }

        public void Update()
        {
            UpdateState(player);
        }

        /// <summary>
        /// Purpose: Updates the states of the enemy
        /// 目的：敵の状態を更新します
        /// </summary>
        /// <param name="playerObj">Reference to the player object</param>
        public void UpdateState(GameObject playerObj)
        {
            float dist = (transform.position - playerObj.transform.position).magnitude;

            switch (enemyState)
            {
                case EnemyStates.Idle:
                    if (dist < 5f) enemyState = EnemyStates.Attack;
                    break;
                case EnemyStates.Attack:
                    if (dist >= 5f) enemyState = EnemyStates.Idle;
                    break;
            }

            DoState(enemyState);
        }

        /// <summary>
        /// Purpose: Explains what each state does on the base level
        /// 目的：各州が基本レベルで何をするかを説明します
        /// </summary>
        /// <param name="currentState">Gets the reference to the current state</param>
        protected void DoState(EnemyStates currentState)
        {
            switch (currentState)
            {
                case EnemyStates.Idle:
                    debugSprite.color = Color.green;
                    EnemyIdle();
                    break;
                case EnemyStates.Attack:
                    debugSprite.color = Color.red;
                    EnemyAttack();
                    break;
            }

        }

        /// <summary>
        /// Purpose: Allows for the enemy to move.
        /// 目的：敵が移動できるようにします。
        /// </summary>
        public void Move()
        {
            Vector3 pos = transform.position;
            Vector3 vel = new Vector3(moveSpeed * direction, 0, 0);
            pos += vel;

            transform.position = pos;
        }

        /// <summary>
        /// Purpose: Finds the direction of the player
        /// 目的：プレイヤーの方向を見つけます
        /// </summary>
        /// <returns></returns>
        public void FindPlayerDirection()
        {
            float adj = (transform.position.x - player.transform.position.x);

            if (adj < 0) direction = 1;
            else direction = -1;

        }

        public virtual void EnemyAttack()
        {
            FindPlayerDirection();
            Move();

        }

        public virtual void EnemyIdle()
        {
            Move();
        }

        public void RandomDirection()
        {
            int rand = Random.Range(0, 1);
            if (rand == 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Block"))
            {
                direction *= -1;
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D collision) { }
    }
}
