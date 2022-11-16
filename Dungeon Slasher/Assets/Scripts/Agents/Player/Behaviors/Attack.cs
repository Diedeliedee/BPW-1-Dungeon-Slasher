using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class Attack : State
        {
            [SerializeField] private float m_attackDistance = 5f;
            [SerializeField] private float m_attackTime = 5f;
            [SerializeField] private float m_squaredQuiteRange = 5f;

            private Vector2 m_endPosition = Vector2.zero;
            private Vector2 m_velocity = Vector2.zero;

            private void StartAttack(Context context)
            {
                var input3 = new Vector3(Controls.rightInput.x, context.transform.position.y, Controls.rightInput.y);

                m_endPosition = context.transform.position + input3 * m_attackDistance;
            }

            private void TickAttack(float _deltaTime)
            {
                var position = Position;
                Position = Vector2.SmoothDamp(position, m_endPosition, ref m_velocity, m_attackTime, Mathf.Infinity, _deltaTime);

                if ((m_endPosition - position).sqrMagnitude > m_squaredQuiteRange) return;
                m_state = State.Moving;
            }
        }
    }
}
