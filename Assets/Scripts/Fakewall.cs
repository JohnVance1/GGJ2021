using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fakewall : MonoBehaviour
{
    Rigidbody2D rb;
    Color color;
    private void Awake()
    {
        rb = transform.gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
