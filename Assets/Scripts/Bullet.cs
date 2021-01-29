using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Transform trans;
    public float angle;
    private void Awake()
    {
        trans = transform;
    }
    void Update()
    {
        Vector3 pos = trans.position;
        pos.x -= speed * Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime;
        pos.y -= speed * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime;
        trans.position = pos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            //プレイヤーにダメージを与える処理
            Destroy(this.gameObject);
        }
    }
}
