using UnityEngine;

namespace MapLogic
{
    // todo Seems the script is not used?
    public class Move : MonoBehaviour
    {
        [SerializeField] Vector2 player;
        [SerializeField] Vector2 Camera;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.position = player;
                //Processing that damages the player
            }
        }
    }
}
