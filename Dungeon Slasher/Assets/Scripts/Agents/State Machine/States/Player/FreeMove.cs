using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        [System.Serializable]
        public class FreeMove : MovementState
        {
            public PlayerControls controls = null;

            public override void OnStart()
            {
                SetBehaviors(new Control(controls));
            }

            public override void OnTick()
            {
                if (controls.slashButtonPressed)
                {
                    parent.SwitchToState<Attack>().SetAttackDirection(Calc.RotateVector2(controls.rightInput, 45f));
                    return;
                }

                TickMovement();
            }
        }
    }
}