using UnityEngine;

public class EnemyJump : EnemyBasic
{
    EnemyStates enemyJumpState = EnemyStates.Idle;
    private Rigidbody2D rb;
    private bool jumping;
    private int jumpPower;
    private float jumpDelay = 2.0f;
    private float timeDelay = 0.0f;


    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        jumping = true;
        jumpPower = 10;
        jumpDelay = 2.0f;
        timeDelay = 0.0f;
    }

    public override void EnemyAttack()
    {
        FindPlayerDirection();
        JumpMove();
    }

    public override void EnemyIdle()
    {
        JumpMove();
    }

    public void JumpMove()
    {
        //float jumpDirection = Random.Range(-1, 2);

        int jumpAngle = Random.Range(30, 75);
        Vector2 jumpVector = Rotate(Vector2.up, direction * jumpAngle * Mathf.Deg2Rad);

        if (!jumping && CanJump())
        {
            //yield return new WaitForSeconds(5.0f);
            jumpPower = 200;
            rb.AddForce(jumpVector * jumpPower);

            timeDelay = Time.time + jumpDelay;

        }
        Debug.Log(jumping);
    }

    public bool CanJump()
    {
        return (Time.time > timeDelay);
    }

    public static Vector2 Rotate(Vector2 v, float radians)
    {
        var ca = Mathf.Cos(radians);
        var sa = Mathf.Sin(radians);
        return new Vector2(ca * v.x + sa * v.y, sa * v.x + ca * v.y);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Block"))
        {            
            jumping = false;
            //rb.velocity = Vector2.zero;
            //Speed = 0;
        }

    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        if (collision.gameObject.CompareTag("Block"))
        {
            jumping = true;
        }
    }
}
