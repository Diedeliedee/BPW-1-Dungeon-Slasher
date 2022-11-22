using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Class responsible for iteratively moving the character.
    /// </summary>
    [System.Serializable]
    public class Movement
    {
        [SerializeReference] private CharacterController m_controller = null;

        //  Run-time:
        private Vector2 m_currentVelocity = Vector2.zero;
        private Vector2 m_currentSteering = Vector2.zero;

        //  Constants:
        private const float m_epsilon = 0.05f;

        #region Properties

        public Vector2 velocity { get => m_currentVelocity; }
        public Collider collider { get => m_controller; }

        #endregion

        #region Controllable Movement

        /// <summary>
        /// Moves the agent iteratively towards a flat 2D position, generated by the passed in desired velocity.
        /// </summary>
        /// <param name="desiredVelocity">The velocity the agents shall aim for.</param>
        /// <param name="grip">How much grip the agent has in it's movement.</param>
        /// <param name="deltaTime">Seconds since the last frame passed.</param>
        public void MoveVelocity(float deltaTime, Vector2 desiredVelocity, float grip, float rotationSmoothening)
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

            //  Rotating.
            RotateTo(deltaTime, m_currentVelocity, rotationSmoothening);
        }

        private void RotateTo(float deltaTime, Vector2 direction, float smoothening)
        {
            var target = Quaternion.LookRotation(Calc.FlatToVector(direction), Vector3.up);
            var current = m_controller.transform.rotation;
            var eulers = current.eulerAngles;

            //  If smoothening is non-existent, simply alter the rotation to the target.
            if (smoothening <= 0f)
            {
                eulers.y = target.eulerAngles.y;
                m_controller.transform.eulerAngles = eulers;
                return;
            }

            //  Interpolate otherwise.
            eulers.y = Quaternion.Slerp(current, target, deltaTime / smoothening).eulerAngles.y;
            m_controller.transform.eulerAngles = eulers;
        }

        #endregion

        #region Physics Movement

        /// <summary>
        /// Moves the agents based on it's current velocity, altered with passed in drag.
        /// </summary>
        /// <param name="drag">The drag the agent has in it's physics based movement.</param>
        /// <param name="deltaTime">Seconds since the last frame passed.</param>
        public void TickPhysics(float deltaTime, float drag)
        {
            var dampenedSpeed = m_currentVelocity.magnitude - drag * deltaTime;

            if (dampenedSpeed < 0f) dampenedSpeed = 0f;
            m_currentSteering = Vector2.zero;
            m_currentVelocity = Vector2.ClampMagnitude(m_currentVelocity, dampenedSpeed);
            m_controller.Move(Calc.FlatToVector(m_currentVelocity * deltaTime, 0f));
        }

        /// <summary>
        /// Adds the passed in velocity on top of the current velocity.
        /// </summary>
        public void AddVelocity(Vector2 velocity)
        {
            m_currentVelocity += velocity;
        }

        /// <summary>
        /// Replaces the current velocity with the new passed in velocity.
        /// </summary>
        public void SetVelocity(Vector2 velocity)
        {
            m_currentVelocity = velocity;
        }

        #endregion

        public void DrawGizmos(Vector3 position)
        {
            var velocity3 = Calc.FlatToVector(m_currentVelocity, position.y);
            var steering3 = Calc.FlatToVector(m_currentSteering, position.y);

            GizmoTools.DrawLine(position, position + steering3, Color.red, 0.5f);
            GizmoTools.DrawLine(position, position + velocity3, Color.green, 0.75f);
        }
    }
}