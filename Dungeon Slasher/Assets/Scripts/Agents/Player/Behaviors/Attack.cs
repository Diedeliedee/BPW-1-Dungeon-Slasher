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
                var input3 = new Vector3(Controls.rightInput.x, blackBoard.transform.position.y, Controls.rightInput.y);

                m_endPosition = blackBoard.transform.position + input3 * m_attackDistance;
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
        }
    }
}
