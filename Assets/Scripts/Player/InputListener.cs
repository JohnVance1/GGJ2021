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
                //I added it because I want the Jump key input state.
                //Assy
                pl.JumpKeyPressed = true;
            }

            if (Input.GetKeyDown(ShootKeyCode))
            {
                if (debug) Debug.Log("player shoot key pressed.");
                pl.ShootKeyPressed = true;
            }

            if (Input.GetKeyDown(ClingKeyCode))
            {
                if (debug) Debug.Log("player cling key pressed.");
                pl.ClingKeyPressed = true;
                //pl.Cling();
            }

            if (Input.GetKeyDown(LeftMoveKeyCode))
            {
                if (debug) Debug.Log("player ← move key pressed.");
                pl.FaceTo(PlayerDirection.Left);
                // todo is this necessar? did I have a problem with flipX?...
                //I wanted to add a simple Rush and Climb hit detection as children, 
                //so I made this as a test to see how I could implement it quickly. 

                //I was going to take advantage of the fact that changing the angle of the player also moves the child data,
                // but I'll try other methods.

                //I'm sorry I changed it without reporting it.
                //Assy
                //pl.PlayerAngleChangeLeft();
                pl.MoveKeyPressed = true; // trigger movement
            }

            if (Input.GetKeyDown(RightMoveKeyCode))
            {
                if (debug) Debug.Log("player → move key pressed.");
                pl.FaceTo(PlayerDirection.Right);
                // todo same?
                //pl.PlayerAngleChangeRight();
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
            if (Input.GetKeyUp(ClingKeyCode)) pl.ClingKeyPressed = false;
        }
    }
}