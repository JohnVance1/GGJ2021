// Bind Keys to certain input action
// @author Lingxiao

using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class InputListener : MonoBehaviour
    {
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
        // todo change key binding
        private void KeyPress()
        {
            if (Input.GetKeyDown(LeftMoveKeyCode))
            {
                pl.FaceTo(PlayerDirection.Left);
                pl.MoveKeyPressed = true; // trigger movement
                return;
            }

            if (Input.GetKeyDown(RightMoveKeyCode))
            {
                pl.FaceTo(PlayerDirection.Right);
                pl.MoveKeyPressed = true; // trigger movement
                return;
            }

            if (Input.GetKeyDown(JumpKeyCode))
            {
                pl.Jump();
                return;
            }
        }

        // todo replace hardcoded KeyCode to actual freely bind keycodes!
        // todo this surely wrong
        private void KeyUp()
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                pl.MoveKeyPressed = false;
                return;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                pl.MoveKeyPressed = false;
                return;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                pl.JumpKeyPressed = false;
                return;
            }
        }
    }
}