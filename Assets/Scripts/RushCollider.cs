using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushCollider : MonoBehaviour
{
    //範囲内に入っているオブジェクトを格納する
    //Store objects that are in the range.
    //In HitObject
    public List<GameObject> NowColliderObject = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyName"))
        {
            NowColliderObject.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyName"))
        {
            NowColliderObject.Remove(collision.gameObject);
        }
    }

}
