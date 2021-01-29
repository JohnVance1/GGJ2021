using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHitCheck : MonoBehaviour
{
    public bool HitFlag = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitFlag = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitFlag = true;
    }
}
