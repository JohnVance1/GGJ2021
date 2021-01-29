using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: John Vance
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBasic : MonoBehaviour
{
    #region Variables
    private SpriteRenderer debugSprite;
    [SerializeField]
    private GameObject player;

    
    #endregion



    public enum EnemyStates
    {
        Idle,
        Attack

    }

    EnemyStates enemyState = EnemyStates.Idle;

    public void Start()
    {
        debugSprite = GetComponent<SpriteRenderer>();

    }

    public void Update()
    {
        UpdateState(player);

    }

    /// <summary>
    /// Purpose: Updates the states of the enemy
    /// </summary>
    /// <param name="playerObj">Reference to the player object</param>
    public void UpdateState(GameObject playerObj)
    {
        float dist = (transform.position - playerObj.transform.position).magnitude;

        switch (enemyState)
        {
            case EnemyStates.Idle:
                if(dist < 5f)
                {
                    enemyState = EnemyStates.Attack;
                }
                break;
            case EnemyStates.Attack:
                if (dist >= 5f)
                {
                    enemyState = EnemyStates.Idle;
                }
                break;



        }

        DoState(enemyState);

    }

    /// <summary>
    /// Purpose: Explains what each state does on the base level
    /// </summary>
    /// <param name="currentState">Gets the reference to the current state</param>
    public void DoState(EnemyStates currentState)
    {
        switch (currentState)
        {
            case EnemyStates.Idle:
            
                debugSprite.color = Color.green;
                Move(1);
                break;
            case EnemyStates.Attack:
                debugSprite.color = Color.red;
                if(FindPlayerDirection() < 0)
                {
                    Move(1);

                }
                else
                {
                    Move(-1);

                }

                break;
            
        } 

    }

    /// <summary>
    /// Purpose: Allows for the enemy to move
    /// </summary>
    /// <param name="dir"></param>
    public void Move(float dir)
    {
        Vector3 pos = transform.position;
        Vector3 vel = new Vector3(0.01f * dir, 0, 0);
        pos += vel;

        transform.position = pos;
    }

    /// <summary>
    /// Purpose: Finds the direction of the player
    /// </summary>
    /// <returns></returns>
    public float FindPlayerDirection()
    {
        float adj = (transform.position.x - player.transform.position.x);
        
        return adj;
    }


    
}
