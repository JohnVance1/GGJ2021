using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject setPlayerObject;

    [SerializeField]
    GameObject setShootObject;

    [SerializeField]
    Rigidbody2D SetPlayerRigidbody2D;

    public int CreateShootCount = 5;

    public float DefaultShootSpeed = 1.0f;

    public float DefaultShootTime = 1.0f;

    public List<ShootData> ListShoot = null;



    [System.Serializable]
    public class ShootData {
        //Object
        public GameObject shootObject;
        //HitCheck 
        public ShootHitCheck shootHitCheck;
        //MoveVector
        public Vector3 moveVec;
        //MoveSpeed
        public float moveSpeed;
        // if Time <= 0  SetActive false
        public float shootTime;
        //SetActive false = false
        //SetActive true = true
        public bool shootNow;

    }


    private void Awake()
    {
        ListShoot = new List<ShootData>();

        for (int i = 0; i < CreateShootCount; i++)
        {
            ShootData Data = new ShootData() {
                shootObject = Instantiate<GameObject>(setShootObject),
                shootHitCheck = null,
                moveVec = Vector3.zero,
                moveSpeed = DefaultShootSpeed,
                shootTime = DefaultShootTime,
                shootNow = false,
            };

            Data.shootHitCheck = Data.shootObject.GetComponent<ShootHitCheck>();

            Data.shootObject.SetActive(false);

            ListShoot.Add(Data);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //ShoootSample
            ShootStart(setPlayerObject.transform.position, Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            //ShoootSample
            ShootStart(setPlayerObject.transform.position, Vector2.right);
        }
    }

    private void FixedUpdate()
    {
        foreach (var ShootData in ListShoot)
        {
            if (ShootData.shootHitCheck.HitFlag)
            {
                ShootData.shootObject.SetActive(false);
                ShootData.shootNow = false;
                continue;
            }

            if(ShootData.shootNow == true)
            {
                if(ShootData.shootTime <= 0)
                {
                    ShootData.shootObject.SetActive(false);
                    ShootData.shootNow = false;
                    continue;
                }

                ShootData.shootObject.transform.position += ShootData.moveVec * ShootData.moveSpeed;

                ShootData.shootTime -= Time.deltaTime;

            }
        }

    }
    public void ShootStart(Vector3 _startPositon,Vector2 _moveVec)
    {
        foreach(var ShootData in ListShoot)
        {
            if (ShootData.shootNow == false)
            {
                Debug.Log("ShootStart!");
                ShootData.shootObject.transform.position = _startPositon;

                ShootData.moveVec = _moveVec;

                ShootData.moveSpeed = DefaultShootSpeed;

                ShootData.shootTime = DefaultShootTime;

                ShootData.shootObject.SetActive(true);

                ShootData.shootNow = true;
                return;
            }
        }
        Debug.Log("NoShoot");
    }

}
