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

            private Vector2 m_currentVelocity = Vector2.zero;
            private Vector2 m_currentSteering = Vector2.zero;

            private const float m_epsilon = 0.05f;

            public Vector2 velocity { get => m_currentVelocity; }
            public Collider collider { get => m_controller; }

            public void MoveVelocity(Vector2 desiredVelocity, float grip, float deltaTime)
            {
                //  Calculating steering.
                m_currentSteering = desiredVelocity - m_currentVelocity;
                m_currentSteering *= grip;

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

            public void TickPhysics(float drag, float deltaTime)
            {
                var dampenedSpeed = m_currentVelocity.magnitude - drag * deltaTime;

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