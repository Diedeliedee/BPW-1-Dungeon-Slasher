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

            public static void CheckInput(Vector3 position)
            {
                leftInput = GetLeftInput();
                rightInput = GetRightInput(position);
                slashButtonPressed = GetSlashButtonPressed();
            }

            private static Vector2 GetLeftInput()
            {
                return Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
            }

            private static Vector2 GetRightInput(Vector3 position)
            {
                return (Input.mousePosition - Camera.main.WorldToScreenPoint(position)).normalized;
            }

            private static bool GetSlashButtonPressed()
            {
                return Input.GetButtonDown("Fire1");
            }
        }
    }
}
