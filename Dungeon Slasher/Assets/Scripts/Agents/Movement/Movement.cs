using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        public class Movement
        {
            private float m_acceleration = 0f;
            private float m_speed = 0f;
            private float m_drag = 0f;

            private Vector2 m_currentVelocity = Vector2.zero;
            private Vector2 m_currentAcceleration = Vector2.zero;

            private CharacterController m_controller = null;

            public float acceleration { get => m_acceleration; set => m_acceleration = value; }
            public float speed { get => m_speed; set => m_speed = value; }
            public float drag { get => m_drag; set => m_drag = value; }

            public Movement(CharacterController controller, AgentSettings settings)
            {
                m_acceleration = settings.baseAcceleration;
                m_speed = settings.maxSpeed;
                m_drag = settings.baseDrag;

                m_controller = controller;
            }

            public Vector2 MoveDirection(Vector2 direction, float deltaTime)
            {
                //  Adding force.
                if (direction.sqrMagnitude > 0f && m_speed > 0f)
                {
                    /*
                    var currentSpeed = m_currentVelocity.magnitude;
                    var fromMax = m_speed - currentSpeed;
                    */
                    m_currentAcceleration = direction.normalized * m_acceleration;
                    m_currentVelocity = Vector2.ClampMagnitude(m_currentVelocity + m_currentAcceleration * deltaTime, m_speed);
                }
                else
                {
                    m_currentAcceleration = Vector2.zero;
                }

                //  Applying drag.
                m_currentVelocity = Vector2.ClampMagnitude(m_currentVelocity, m_currentVelocity.magnitude - (m_drag * deltaTime));

                m_controller.Move(Calc.FlatToVector(m_currentVelocity) * deltaTime);
                return m_currentVelocity;
            }

            public void DrawGizmos(Vector3 position)
            {
                var velocity3 = Calc.FlatToVector(m_currentVelocity, position.y);
                var acceleration3 = Calc.FlatToVector(m_currentAcceleration, position.y);

                GizmoTools.DrawLine(position, position + acceleration3, Color.red, 0.5f);
                GizmoTools.DrawLine(position, position + velocity3, Color.green, 0.75f);
            }
        }
    }
}