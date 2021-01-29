using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Vector2 player;
    [SerializeField] Vector2 Camera;
    [SerializeField] GameObject CameraObject;
    public bool move;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = player;
            if (!move)
            {
                //プレイヤーにダメージを与える処理
            }
            else
            {
                CameraObject.gameObject.transform.position = Camera;
            }
        }
    }
}
