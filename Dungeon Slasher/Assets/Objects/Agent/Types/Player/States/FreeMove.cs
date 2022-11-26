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
            public override void Initialize(FStateMachine parent)
            {
                base.Initialize(parent);
                SetBehaviors(new Control(PlayerControls.inputRotation));
            }

            public override void OnEnter()
            {
                CrossFadeAnimation(0.3f);
            }

            public override void OnTick()
            {
                if (PlayerControls.slashButtonPressed)
                {
                    parent.SwitchToState<LeftAttack>().InitiateAttack(Controls.rightInput);
                    return;
                }

                TickMovement();
            }
        }
    }
}