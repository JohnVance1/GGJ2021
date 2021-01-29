using System.Collections;
using UnityEngine;

namespace MapLogic
{
    public class Launchpad : MonoBehaviour
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

        // shoot bullets and they get destroyed in Bullet.cs
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
}