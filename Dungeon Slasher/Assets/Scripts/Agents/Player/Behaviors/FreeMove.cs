using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class FreeMove : State
        {
            public override void OnTick()
            {
                if (Controls.slashButtonPressed)
                {
                    parent.SwitchToState(typeof(Attack));
                    return;
                }

                blackBoard.movement.MoveDirection(Controls.leftInput.normalized, blackBoard.deltaTime);
            }
        }
    }
}