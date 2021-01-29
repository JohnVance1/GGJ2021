using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchpad : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    Transform trans;
    Vector2 pos;
    public float interval;
    private void Awake()
    {
        pos = transform.position;
        trans = transform;
        StartCoroutine(Shot());
    }
    IEnumerator Shot()
    {
        while (true)
        {
            var obj = Instantiate(bullet, pos, Quaternion.identity);
            obj.GetComponent<Bullet>().angle = trans.rotation.eulerAngles.z;
            yield return new WaitForSeconds(interval);
        }
    }
}
