using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        [System.Serializable]
        public class Movement
        {
            [SerializeField] private CharacterController m_controller = null;

            private float m_speed = 0f;
            private float m_grip = 0f;
            private float m_drag = 0f;

            private Vector2 m_currentVelocity = Vector2.zero;
            private Vector2 m_currentSteering = Vector2.zero;

            private const float m_epsilon = 0.05f;

            public Vector2 velocity { get => m_currentVelocity; }
            public float speed { get => m_speed; set => m_speed = value; }
            public float grip { get => m_grip; set => m_grip = value; }
            public float drag { get => m_drag; set => m_drag = value; }
            public Collider collider { get => m_controller; }

            public void MoveDirection(Vector2 direction, float deltaTime)
            {
                var desiredVelocity = direction * m_speed;

                //  Calculating steering.
                m_currentSteering = desiredVelocity - m_currentVelocity;
                m_currentSteering *= m_grip;

                //  Calculating velocity.
                m_currentVelocity += m_currentSteering;
                if (m_currentVelocity.sqrMagnitude < m_epsilon)
                {
                    m_currentVelocity = Vector2.zero;
                    return;
                }

                //  Applying velocity.
                m_controller.Move(Calc.FlatToVector(m_currentVelocity * deltaTime, 0f));
            }

            public void TickPhysics(float deltaTime)
            {
                var dampenedSpeed = m_currentVelocity.magnitude - m_drag * deltaTime;

                if (dampenedSpeed < 0f) dampenedSpeed = 0f;
                m_currentSteering = Vector2.zero;
                m_currentVelocity = Vector2.ClampMagnitude(m_currentVelocity, dampenedSpeed);
                m_controller.Move(Calc.FlatToVector(m_currentVelocity * deltaTime, 0f));
            }

            public void AddVelocity(Vector2 velocity)
            {
                m_currentVelocity += velocity;
            }

            public void SetVelocity(Vector2 velocity)
            {
                m_currentVelocity = velocity;
            }

            public void ResetProperties()
            {
                m_speed = 0f;
                m_grip = 0f;
                m_drag = 0f;
            }

            public void DrawGizmos(Vector3 position)
            {
                var velocity3 = Calc.FlatToVector(m_currentVelocity, position.y);
                var steering3 = Calc.FlatToVector(m_currentSteering, position.y);

                GizmoTools.DrawLine(position, position + steering3, Color.red, 0.5f);
                GizmoTools.DrawLine(position, position + velocity3, Color.green, 0.75f);
            }
        }
    }
}