using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        private static class Controls
        {
            public static Vector2 leftInput { get; private set; }
            public static Vector2 rightInput { get; private set; }
            public static bool slashButtonPressed { get; private set; }

            public static void CheckInput()
            {
                leftInput = GetLeftInput();
                rightInput = GetRightInput();
                slashButtonPressed = Input.GetKeyDown(KeyCode.Space);
            }

            private static Vector2 GetLeftInput()
            {
                var rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                return Calc.RotateVector2(rawInput, 45f);
            }

            private static Vector2 GetRightInput()
            {
                var input = Vector2.zero;

                if (Input.GetKeyDown(KeyCode.LeftArrow)) input.x--;
                if (Input.GetKeyDown(KeyCode.RightArrow)) input.x++;
                if (Input.GetKeyDown(KeyCode.UpArrow)) input.y--;
                if (Input.GetKeyDown(KeyCode.DownArrow)) input.y++;
                return input;
            }
        }
    }
}
