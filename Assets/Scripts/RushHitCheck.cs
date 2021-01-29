using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic
{

    public class RushHitCheck : MonoBehaviour
    {
        //Editor　Inspector Count Watch
        public int DebugHitCounts;
        //ラッシュエリア内の敵オブジェクトを返す
        //Return enemy objects in the rush area.
        private readonly List<GameObject> ListRushAreaHitEnemyObjects = new List<GameObject>();
        //or
        //public List<EnemyBasic> ListEnemy = new List<EnemyBasic>();
        public List<GameObject> GetRushAreainEnemyObjects()
        {
            return ListRushAreaHitEnemyObjects;
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
