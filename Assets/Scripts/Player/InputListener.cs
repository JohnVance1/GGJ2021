// Bind Keys to certain input action
// @author Lingxiao

using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class InputListener : MonoBehaviour
    {
        public bool debug = true;

        #region KeyPressCode
        // default keyboard bind
        public KeyCode LeftMoveKeyCode = KeyCode.LeftArrow;
        public KeyCode RightMoveKeyCode = KeyCode.RightArrow;
        public KeyCode JumpKeyCode = KeyCode.UpArrow;
        public KeyCode RushKeyCode = KeyCode.DownArrow;
        public KeyCode ShootKeyCode = KeyCode.Z;
        public KeyCode ClingKeyCode = KeyCode.X;
        #endregion

        private Player pl;

        private void Awake()
        {
            pl = GetComponent<Player>();
        }

        private void Update()
        {
            KeyPress();
            KeyUp();
        }


        // convert key press to player action
        private void KeyPress()
        {
            if (Input.GetKeyDown(JumpKeyCode))
            {
                if (debug) Debug.Log("player jump key pressed.");
                pl.Jump();
            }

            if (Input.GetKeyDown(ShootKeyCode))
            {
                if (debug) Debug.Log("player shoot key pressed.");
                pl.ShootKeyPressed = true;
            }

            if (Input.GetKeyDown(LeftMoveKeyCode))
            {
                if (debug) Debug.Log("player ← move key pressed.");
                pl.FaceTo(PlayerDirection.Left);
                pl.MoveKeyPressed = true; // trigger movement
            }

            if (Input.GetKeyDown(RightMoveKeyCode))
            {
                if (debug) Debug.Log("player → move key pressed.");
                pl.FaceTo(PlayerDirection.Right);
                pl.MoveKeyPressed = true; // trigger movement
            }
        }

        // listen to freely bind keycodes, set player status
        private void KeyUp()
        {
            if (Input.GetKeyUp(LeftMoveKeyCode)) pl.MoveKeyPressed = false;
            if (Input.GetKeyUp(RightMoveKeyCode)) pl.MoveKeyPressed = false;
            if (Input.GetKeyUp(JumpKeyCode)) pl.JumpKeyPressed = false;
            if (Input.GetKeyUp(ShootKeyCode)) pl.ShootKeyPressed = false;
        }
    }
}