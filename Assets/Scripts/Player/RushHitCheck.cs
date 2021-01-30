using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic
{

    public class RushHitCheck : MonoBehaviour
    {
        //Editor Inspector Count Watch
        public int DebugHitCounts;
        //Return enemy objects in the rush area.
        private readonly List<GameObject> ListRushAreaHitEnemyObjects = new List<GameObject>();
        //or
        //public List<EnemyBasic> ListEnemy = new List<EnemyBasic>();
        private BoxCollider2D r_bcoll;

        private void Awake()
        {
            r_bcoll = GetComponent<BoxCollider2D>();
        }

        public List<GameObject> GetRushAreainEnemyObjects()
        {
            return ListRushAreaHitEnemyObjects;
        }

        //RushArea AngleChange
        public void ChangeOffset(Vector2 _moveVec)
        {
            if (r_bcoll .offset.x< 0 && _moveVec.x >0)
            {
                r_bcoll.offset = -r_bcoll.offset;
            }
            else if (r_bcoll.offset.x > 0 && _moveVec.x < 0)
            {
                r_bcoll.offset = -r_bcoll.offset;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if Hits Enemy Enter // now HitAllObject
            if (true)
            {
                //list Add EnemyObjectData
                ListRushAreaHitEnemyObjects.Add(collision.gameObject);
            }
            // if (collision.CompareTag("EnemyTagName"))
            /*
            if(true)
            {
                var enemybasic = collision.gameObject.GetComponent<EnemyBasic>();

                if (enemybasic != null)
                {
                    ListEnemy.Add(enemybasic);
                }
            }
            */

            DebugHitCounts = ListRushAreaHitEnemyObjects.Count;

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //if Hits Enemy Exit // now HitAllObject
            if (true)
            {
                //list Remove EnemyObjectData
                ListRushAreaHitEnemyObjects.Remove(collision.gameObject);
            }
            /*
            // if (collision.CompareTag("EnemyTagName"))
            if (true)
            {
                var enemybasic = collision.gameObject.GetComponent<EnemyBasic>();

                if (enemybasic != null)
                {
                    ListEnemy.Add(enemybasic);
                }
            }
            */


            DebugHitCounts = ListRushAreaHitEnemyObjects.Count;


        }

    }
}
