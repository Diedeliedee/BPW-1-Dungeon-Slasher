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
        public static class Controls
        {
            public static Vector2 leftInput { get; private set; }
            public static Vector2 rightInput { get; private set; }
            public static bool slashButtonPressed { get; private set; }

            public static void CheckInput(Blackboard blackboard)
            {
                leftInput = GetLeftInput();
                rightInput = GetRightInput(blackboard);
                slashButtonPressed = GetSlashButtonPressed();
            }

            private static Vector2 GetLeftInput()
            {
                var rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                return Calc.RotateVector2(rawInput, 45f);
            }

            private static Vector2 GetRightInput(Blackboard blackboard)
            {
                var input = Input.mousePosition - Camera.main.WorldToScreenPoint(blackboard.transform.position);

                return Calc.RotateVector2(input, 45f);
            }

            private static bool GetSlashButtonPressed()
            {
                return Input.GetButtonDown("Fire1");
            }
        }
    }
}
