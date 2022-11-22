using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        [System.Serializable]
        public class MovementState : AgentState
        {
            //  Properties:
            [SerializeField] private float m_speed = 10f;
            [SerializeField] private float m_grip = 0.1f;

            private BehaviorHandler m_behavior = null;

            /// <summary>
            /// Called whenever the finite state machine the state is in, is created.
            /// </summary>
            public override void Initialize(FStateMachine parent)
            {
                base.Initialize(parent);
                m_behavior = new BehaviorHandler();
            }

            /// <summary>
            /// Sets the movement behaviors of the current state.
            /// </summary>
            protected void SetBehaviors(params Behavior[] behaviors)
            {
                m_behavior.SetBehaviors(behaviors);
            }

            /// <summary>
            /// Calculates the desired velocity based on the set movement behaviors, and moves the agent.
            /// </summary>
            protected void TickMovement()
            {
                var context = new Behavior.Context(blackBoard.deltaTime, m_speed, blackBoard.flatPosition, blackBoard.movement.velocity);
                var desiredVelocity = Calc.RotateVector2(m_behavior.GetDesiredVelocity(context), 45f);

                blackBoard.movement.MoveVelocity(blackBoard.deltaTime, desiredVelocity, m_grip);
            }
        }
    }
}
