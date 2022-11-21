using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        [System.Serializable]
        public class FreeMove : AgentState
        {
            //  Properties:
            [SerializeField] private float m_speed = 10f;
            [SerializeField] private float m_grip = 0.1f;

            public override void OnStart()
            {
                behavior.SetBehaviors(new Control());
            }

            public override void OnTick()
            {
                if (Controls.slashButtonPressed)
                {
                    parent.SwitchToState(typeof(Attack));
                    return;
                }

                blackBoard.movement.MoveVelocity(behavior.GetDesiredVelocity(blackBoard.flatPosition, m_speed), m_grip, blackBoard.deltaTime);
            }
        }
    }
}