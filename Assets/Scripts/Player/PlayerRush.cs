using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerLogic
{
    public class PlayerRush : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private Rigidbody2D rigidbody2D;

        public float RushSpeed = 0.01f;

        public float CreateAfterimageWaitTime = 0.1f;

        public int CreateAfterimageCount = 5;

        public bool RushFlag = false;

        private Vector3 moveVec;

        private List<GameObject> AfterimageObjects = new List<GameObject>();

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {

            if (RushFlag)
            {
                transform.position += moveVec * RushSpeed;
            }
        }
        /// <summary>
        /// Running a rush
        /// </summary>
        public void PushMoveStart()
        {
            moveVec = Vector3.right;
            if (RushFlag) return;
            StartCoroutine(CreateAfterimage());
        }
        public void PushMoveStart(Vector3 mVec)
        {
            moveVec = mVec;
            if (RushFlag) return;
            StartCoroutine(CreateAfterimage());
        }
        public void PushMoveStart(float mVec)
        {
            moveVec = new Vector3(mVec, 0.0f, 0.0f);
            if (RushFlag) return;
            StartCoroutine(CreateAfterimage());
        }
        IEnumerator CreateAfterimage()
        {
            Debug.Log("RushStart2");

            int CreateCount = 0;

            RushFlag = true;

            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            while (true)
            {

                Debug.Log("New!");
                GameObject AfterimageObj = new GameObject();
                var render = AfterimageObj.AddComponent<SpriteRenderer>();
                render.sprite = spriteRenderer.sprite;
                render.flipX = moveVec.x < 0;
                AfterimageObj.transform.position = this.transform.position;
                AfterimageObjects.Add(AfterimageObj);
                CreateCount++;

                for (int i = 0; i < AfterimageObjects.Count; i++)
                {
                    var spriteRen = AfterimageObjects[i].GetComponent<SpriteRenderer>();
                    var color = spriteRen.color;
                    //Debug.Log(color.a);
                    color.a = 1.0f / (AfterimageObjects.Count - i);
                    spriteRen.color = color;
                }

                yield return new WaitForSeconds(CreateAfterimageWaitTime);

                if (CreateCount > CreateAfterimageCount)
                {
                    //Debug.Log("CreateEnd");
                    break;
                }

            }
            //yield return new WaitForSeconds(0.5f);

            foreach (var data in AfterimageObjects)
            {
                Destroy(data);
            }
            AfterimageObjects.Clear();

            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            Debug.Log("RushEnd");
            RushFlag = false;

            yield return null;
        }
    }
}