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
            public bool slashButtonPressed { get; private set; }

            public void CheckInput(Blackboard blackboard)
            {
                leftInput = GetLeftInput();
                rightInput = GetRightInput(blackboard);
                slashButtonPressed = GetSlashButtonPressed();
            }

            private Vector2 GetLeftInput()
            {
                return Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
            }

            private Vector2 GetRightInput(Blackboard blackboard)
            {
                return Vector2.ClampMagnitude(Input.mousePosition - Camera.main.WorldToScreenPoint(blackboard.transform.position), 1f);
            }

            private bool GetSlashButtonPressed()
            {
                return Input.GetButtonDown("Fire1");
            }
        }
    }
}
