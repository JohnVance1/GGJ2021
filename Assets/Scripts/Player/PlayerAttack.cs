using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        public bool debug = true;

        [SerializeField]
        GameObject setShootObject;

        public int CreateShootCount = 5;
        public float DefaultShootSpeed = 1.0f;
        public float DefaultShootTime = 1.0f;
        //CoolDonwSetValue
        public float SetShootCollDownTime = 0.1f;
        //Main CoolDown Counter
        private float ShootColllDownCounter;

        private readonly List<ShootData> ListShoot = new List<ShootData>();



        [System.Serializable]
        public class ShootData
        {
            //Bullet Prefab
            public GameObject shootObject;
            //HitCheck 
            public ShootHitCheck shootHitCheck;
            //MoveVector
            public Vector3 moveVec;
            //MoveSpeed
            public float moveSpeed;
            // if Time <= 0  SetActive false
            public float shootTime;
            public bool shootNow;

        }

        // Prepare all bullets and reuse them
        private void Awake()
        {
            for (int i = 0; i < CreateShootCount; i++)
            {
                ShootData Data = new ShootData
                {
                    shootObject = Instantiate(setShootObject),
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

        // Update bullets status
        private void FixedUpdate()
        {
            //CoolDowRecast
            if (ShootColllDownCounter > 0)
            {
                ShootColllDownCounter -= Time.fixedDeltaTime;
            }

            foreach (var data in ListShoot)
            {
                if (data.shootHitCheck.HitFlag)
                {
                    data.shootObject.SetActive(false);
                    data.shootNow = false;
                    data.shootHitCheck.HitFlag = false;
                    if (debug)
                        Debug.Log("OtherObjectHit");
                    continue;
                }

                if (data.shootNow == true)
                {
                    if (data.shootTime <= 0)
                    {
                        data.shootObject.SetActive(false);
                        data.shootNow = false;
                        if (debug)
                            Debug.Log("AttackFalse");
                        continue;
                    }

                    data.shootObject.transform.position += data.moveVec * data.moveSpeed;
                    data.shootTime -= Time.fixedDeltaTime;
                }
            }
        }

        // Shoot a bullet
        public void ShootStart(Vector3 _startPositon, Vector2 _moveVec)
        {
            foreach (var data in ListShoot)
            {
                if (data.shootNow == false)
                {
                    data.shootObject.transform.position = _startPositon;
                    data.moveVec = _moveVec;
                    data.moveSpeed = DefaultShootSpeed;
                    data.shootTime = DefaultShootTime;
                    data.shootObject.SetActive(true);
                    data.shootNow = true;
                    data.shootObject.GetComponent<SpriteRenderer>().flipX = data.moveVec.x < 0;
                    ShootColllDownCounter = SetShootCollDownTime;
                    return;
                }
            }
        }
    }
}
