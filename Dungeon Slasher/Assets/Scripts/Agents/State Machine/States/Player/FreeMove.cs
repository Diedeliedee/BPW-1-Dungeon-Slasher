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
                SetBehaviors(new Control(controls, 45f));
            }

            public override void OnTick()
            {
                if (controls.slashButtonPressed)
                {
                    parent.SwitchToState<Attack>().SetAttack(Calc.RotateVector2(controls.rightInput, 45f), GetType());
                    return;
                }

                TickMovement();
            }
        }
    }
}