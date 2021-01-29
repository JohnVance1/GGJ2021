// @author Assy
// attached to a bullet and check bullet hit.
using UnityEngine;

public class ShootHitCheck : MonoBehaviour
{
    public bool HitFlag = false;

    // todo this should check what it hits: player? enemy? wall?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitFlag = true;
    }
}
