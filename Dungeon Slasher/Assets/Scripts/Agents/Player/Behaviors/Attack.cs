using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class Attack : State
        {
            private Settings m_settings = null;

            private float m_timer = 0f;
            private Vector2 m_startPosition = Vector2.zero;
            private Vector2 m_endPosition = Vector2.zero;

            public Attack(Settings settings)
            {
                m_settings = settings;
            }

            public override void OnEnter()
            {
                m_startPosition = blackBoard.flatPosition;
                m_endPosition = m_startPosition + Controls.rightInput.normalized * m_settings.attackDistance;
            }

            public override void OnTick()
            {
                //  Go to free movement state if action ended.
                if (m_timer > m_settings.attackTime)
                {
                    parent.SwitchToState(typeof(FreeMove));
                    return;
                }

                //  A movement component needs to be added to the agent, but this will do for now.
                m_timer += blackBoard.deltaTime;
                blackBoard.flatPosition = Vector2.LerpUnclamped(m_startPosition, m_endPosition, m_settings.distanceOverTime.Evaluate(m_timer / m_settings.attackTime));
            }

            public override void OnExit()
            {
                m_timer = 0f;
                m_startPosition = Vector2.zero;
                m_endPosition = Vector2.zero;
            }

            public override void OnDrawGizmos()
            {
                var height = blackBoard.transform.position.y;
                var startPosition3D = Calc.FlatToVector(m_startPosition, height);
                var endPosition3D = Calc.FlatToVector(m_endPosition, height);

                GizmoTools.DrawLine(startPosition3D, endPosition3D, Color.red);
                GizmoTools.DrawCircle(endPosition3D, Mathf.Sqrt(m_settings.squaredQuiteRange), Color.green);
            }

            [System.Serializable]
            public class Settings
            {
                public float attackDistance = 5f;
                public float attackTime = 0.05f;
                public float squaredQuiteRange = 1f;
                public AnimationCurve distanceOverTime = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            }
        }
    }
}
