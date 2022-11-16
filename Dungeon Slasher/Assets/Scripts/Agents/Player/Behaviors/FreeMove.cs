using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class FreeMove : State
        {
            private Settings m_settings = null;

            private Vector2 m_velocity = Vector2.zero;
            private Vector2 m_acceleration = Vector2.zero;

            public FreeMove(Settings settings)
            {
                m_settings = settings;
            }

            public override void OnTick()
            {
                if (Controls.slashButtonPressed)
                {
                    parent.SwitchToState(typeof(Attack));
                    return;
                }

                var velocity2 = GetVelocity(Controls.leftInput, m_settings.maxSpeed, blackBoard.deltaTime);
                var velocity3 = new Vector3(velocity2.x, 0f, velocity2.y);

                blackBoard.controller.Move(velocity3 * blackBoard.deltaTime);
                m_velocity = velocity2;
            }

            public override void OnExit()
            {
                m_velocity = Vector2.zero;
            }

            /// <returns>The desired velocity altered by the passed in input.</returns>
            private Vector2 GetVelocity(Vector2 input, float speed, float deltaTime)
            {
                return Vector2.SmoothDamp(m_velocity, input.normalized * speed, ref m_acceleration, m_settings.timeToMax, Mathf.Infinity, deltaTime);
            }

            public override void OnDrawGizmos()
            {
                var worldPosition = blackBoard.transform.position;
                var velocity3 = Calc.FlatToVector(m_velocity, worldPosition.y);
                var acceleration3 = Calc.FlatToVector(m_acceleration, worldPosition.y);

                GizmoTools.DrawLine(worldPosition, worldPosition + acceleration3, Color.red, 0.5f);
                GizmoTools.DrawLine(worldPosition, worldPosition + velocity3, Color.green, 0.75f);
            }

            [System.Serializable]
            public class Settings
            {
                public float maxSpeed = 10f;
                public float timeToMax = 0.1f;
            }
        }
    }
}