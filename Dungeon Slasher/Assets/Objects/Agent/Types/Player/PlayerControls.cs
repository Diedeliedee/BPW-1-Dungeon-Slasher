using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        public class PlayerControls : Controls
        {
            public const float inputRotation = 45f;

            public static bool slashButtonPressed { get; private set; }

            public static void CheckInput(Blackboard blackboard)
            {
                leftInput = GetLeftInput();
                rightInput = GetRightInput(blackboard);
                slashButtonPressed = GetSlashButtonPressed();
            }

            private static Vector2 GetLeftInput()
            {
                return Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
            }

            private static Vector2 GetRightInput(Blackboard blackboard)
            {
                return (Input.mousePosition - Camera.main.WorldToScreenPoint(blackboard.transform.position)).normalized;
            }

            private static bool GetSlashButtonPressed()
            {
                return Input.GetButtonDown("Fire1");
            }
        }
    }
}
