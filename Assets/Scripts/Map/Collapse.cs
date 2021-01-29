using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D box;
    float time;
    private void Awake()
    {
        rb = transform.gameObject.GetComponent<Rigidbody2D>();
        box = transform.gameObject.GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }

        if(2.0f <= time)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            box.isTrigger = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            time += Time.deltaTime;
            Debug.Log("Player hit collapse.");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            time = 0.0f;
            Debug.Log("Player leave collapse.");
        }
    }
}
