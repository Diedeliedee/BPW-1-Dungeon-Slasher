using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class Attack : State
        {
            private float m_attackDistance = 5f;
            private float m_attackTime = 0.05f;
            private float m_squaredQuiteRange = 1f;

            private Vector2 m_endPosition = Vector2.zero;
            private Vector2 m_velocity = Vector2.zero;

            public override void OnEnter()
            {
                m_endPosition = blackBoard.flatPosition + Controls.rightInput.normalized * m_attackDistance;
            }

            public override void OnTick()
            {
                var position = blackBoard.flatPosition;

                //  Go to free movement state if within end range. 
                if ((m_endPosition - position).sqrMagnitude < m_squaredQuiteRange)
                {
                    parent.SwitchToState(typeof(Movement));
                    return;
                }

                //  A movement component needs to be added to the agent, but this will do for now.
                blackBoard.flatPosition = Vector2.SmoothDamp(position, m_endPosition, ref m_velocity, m_attackTime, Mathf.Infinity, blackBoard.deltaTime);
            }

            public override void OnDrawGizmos()
            {
                var worldPosition = blackBoard.transform.position;
                var endPosition3D = Calc.FlatToVector(m_endPosition, worldPosition.y);

                GizmoTools.DrawLine(worldPosition, endPosition3D, Color.red);
                GizmoTools.DrawCircle(endPosition3D, Mathf.Sqrt(m_squaredQuiteRange), Color.green);
            }
        }
    }
}
