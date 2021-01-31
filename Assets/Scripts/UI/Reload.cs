using UnityEngine;
using PlayerLogic;

namespace UI
{
    public class Reload : MonoBehaviour
    {
        private Player pl;

        private void Start()
        {
            pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) Click();
        }

        public void Click()
        {
            Debug.Log("Reload");
            if (pl != null) pl.Spawn();
        }
    }
}
