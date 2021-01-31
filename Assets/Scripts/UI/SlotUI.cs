using UnityEngine;
using PlayerLogic;
using UnityEngine.UI;

namespace UI
{
    public class SlotUI : MonoBehaviour
    {
        public delegate void OnPickUpItem(Player pl);
        public static OnPickUpItem PickUpEvent;

        public bool debug;

        private Slot[] slots;
        private Text slotText;

        private void Awake()
        {
            slotText = GameObject.FindGameObjectWithTag("SlotText").GetComponent<Text>();

            slots = GetComponentsInChildren<Slot>();
            Debug.Log("Slots (including disabled): " + slots.Length);
        }

        private void Start()
        {
            Player pl = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            if (pl != null) RefreshSlots(pl);
        }

        private void OnEnable()
        {
            PickUpEvent += RefreshSlots;
        }

        private void OnDisable()
        {
            PickUpEvent -= RefreshSlots;
        }


        private void RefreshSlots(Player pl)
        {
            if (debug)
                Debug.Log("UI: Refresh player slots.");
            foreach (Slot slot in slots) { UpdateSlot(slot, pl); }

            if (debug)
                Debug.Log("UI: Refresh text.");
            slotText.text = "Player Empty Slots: " + (pl.MaxSlots - pl.SlotCount);

            if (debug)
                Debug.Log("UI: Game resume.");
            pl.Resume();
            EnemyLogic.EnemyBasic.ResumeEvent?.Invoke();
        }

        private void UpdateSlot(Slot slot, Player pl)
        {
            if (slot.type == ItemType.Item_Right && pl.enableRightMove) slot.Enable();
            if (slot.type == ItemType.Item_Left && pl.enableLeftMove) slot.Enable();
            if ((slot.type == ItemType.Item_DoubleJump || slot.type == ItemType.Item_Jump) &&
                (pl.enableJump || pl.enableDoubleJump)) slot.Enable();
            if (slot.type == ItemType.Item_Rush && pl.enableRush) slot.Enable();
            if (slot.type == ItemType.Item_Climb && pl.enableCling) slot.Enable();
            if (slot.type == ItemType.Item_Shoot && pl.enableShoot) slot.Enable();
        }


        /// <summary>
        /// Functions to swap key slots but we now just unlock them in order
        /// and no manual adjustment.
        /// </summary>
        //private void Unlock()
        //{
        //    // unlock next slot
        //    throw new System.NotImplementedException();
        //}

        //private void Insert(ItemType item, Slot slot)
        //{
        //    // insert item to empty slot
        //    throw new System.NotImplementedException();
        //}

        //private void Takeout(Slot slot)
        //{
        //    // take out a item from a slot
        //    throw new System.NotImplementedException();
        //}

        //private void Drop(ItemType item)
        //{
        //    // drop an item and spawn it on map
        //    throw new System.NotImplementedException();
        //}
    }
}
