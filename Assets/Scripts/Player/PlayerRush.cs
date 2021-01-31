using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRush : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidbody2D;

    public float AfterimageDestroyTime = 0.5f;

    public float MaxRushTime = 2.0f;

    public float RushSpeed = 1.0f;

    public float CreateAfterimageWaitTime = 1.0f;

    public int CreateAfterimageCount = 5;

    public bool RushFlag = false;

    private List<GameObject> AfterimageObjects = new List<GameObject>();

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {

        if (RushFlag)
        {
            transform.position += Vector3.right * RushSpeed;
        }
    }
    public void PushMoveStart()
    {
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
            AfterimageObj.AddComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            AfterimageObj.transform.position = this.transform.position;
            AfterimageObjects.Add(AfterimageObj);
            CreateCount++;
   
            for(int i = 0; i < AfterimageObjects.Count; i++)
            {
                var spriteRen = AfterimageObjects[i].GetComponent<SpriteRenderer>();
                var color = spriteRen.color;
                //Debug.Log(color.a);
                color.a = 1.0f / (AfterimageObjects.Count - i);
                spriteRen.color = color;
            }

            yield return new WaitForSeconds(0.1f);

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
