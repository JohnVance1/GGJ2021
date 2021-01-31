using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class Slot : MonoBehaviour
    {
        public ItemType type;

        private Image image;

        void Awake()
        {
            image = GetComponent<Image>();
            image.enabled = false;
        }

        public void Enable()
        {
            image.enabled = true;
        }
    }
}
