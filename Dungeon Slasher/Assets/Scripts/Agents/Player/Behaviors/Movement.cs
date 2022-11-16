using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class Movement : State
        {
            [Min(0f)] [SerializeField] private float m_maxSpeed = 10f;
            [Min(0f)] [SerializeField] private float m_timeToMax = 0.25f;
            [Space]
            [SerializeField] private CharacterController m_controller = null;

            private Vector2 m_velocity = Vector2.zero;
            private Vector2 m_acceleration = Vector2.zero;

            public override void OnTick(Context context)
            {
                var velocity2 = GetVelocity(Controls.leftInput, m_maxSpeed, context.deltaTime);
                var velocity3 = new Vector3(velocity2.x, 0f, velocity2.y);

                m_controller.Move(velocity3 * context.deltaTime);
                m_velocity = velocity2;
            }

            public override void OnExit(Context context)
            {
                m_velocity = Vector2.zero;
            }

            /// <returns>The desired velocity altered by the passed in input.</returns>
            private Vector2 GetVelocity(Vector2 input, float speed, float deltaTime)
            {
                return Vector2.SmoothDamp(m_velocity, input.normalized * speed, ref m_acceleration, m_timeToMax, Mathf.Infinity, deltaTime);
            }
        }
    }
}