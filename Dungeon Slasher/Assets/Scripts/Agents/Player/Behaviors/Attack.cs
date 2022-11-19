using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class Attack : State
        {
            //  Properties:
            private float m_attackTreshhold = 3f;   //  The maximum speed until the player can slash.

            private int m_state = 0;
            private Vector2 m_clickOffset = Vector2.zero;

            public override void OnEnter()
            {
                m_clickOffset = Controls.rightInput.normalized * blackBoard.settings.attackDistance;
            }

            public override void OnTick()
            {
                //  Go to free movement state if action ended.
                if (m_timer > blackBoard.settings.attackTime)
                {
                    parent.SwitchToState(typeof(FreeMove));
                    return;
                }

                switch (m_state)
                {
                    case 0:
                        if (blackBoard.movement.velocity.magnitude > m_attackTreshhold) break;

                        m_state = 1;

                }
                blackBoard.movement.TickPhysics(blackBoard.deltaTime);

                //  A movement component needs to be added to the agent, but this will do for now.
                blackBoard.flatPosition = Vector2.LerpUnclamped(m_startPosition, m_clickOffset, blackBoard.settings.distanceOverTime.Evaluate(m_timer / blackBoard.settings.attackTime));
            }

            public override void OnExit()
            {
                m_clickOffset = Vector2.zero;
            }

            public override void OnDrawGizmos()
            {
                var height = blackBoard.transform.position.y;
                var endPosition3D = Calc.FlatToVector(m_clickOffset, height);
            }
        }
    }
}
