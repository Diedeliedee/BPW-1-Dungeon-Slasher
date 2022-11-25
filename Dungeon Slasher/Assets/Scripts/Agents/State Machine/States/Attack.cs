using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        [System.Serializable]
        public class Attack : AgentState
        {
            /// Velocity    = Time          * Drag
            /// Drag        = Velocity      / Time
            /// Drag        = Velocity^2    / (2 * Distance)

            //  Properties:
            [Space]
            [SerializeField] private float m_attackMark = 0.05f;
            [SerializeField] private float m_followThroughMark = 0.2f;
            [SerializeField] private float m_recoverMark = 0.3f;
            [SerializeField] private float m_endMark = 1f;
            [Space]
            [SerializeField] private float m_attackSpeed = 60f;
            [Range(0f, 1f)] [SerializeField] private float m_followThroughPercent = 0.1f;

            //  Run-time:
            private Vector2 m_attackDirection = Vector2.zero;
            private System.Type m_returnState = null;

            private Quaternion m_startRotation = Quaternion.identity;
            private Quaternion m_endRotation = Quaternion.identity;
            private float m_brakeDrag = 0f;
            private float m_attackDrag = 0f;
            private float m_followThroughDrag = 0f;

            private State m_state = State.WindUp;
            private float m_timer = 0f;

            public void SetAttack(Vector2 direction, System.Type returnState)
            {
                var followThroughSpeed = m_attackSpeed * m_followThroughPercent;

                m_attackDirection = direction.normalized;
                m_returnState = returnState;

                m_startRotation = blackBoard.transform.rotation;
                m_endRotation = Quaternion.LookRotation(Calc.FlatToVector(m_attackDirection, blackBoard.transform.position.y));

                m_brakeDrag = blackBoard.movement.velocity.magnitude / m_attackMark;
                m_attackDrag = (m_attackSpeed - followThroughSpeed) / (m_followThroughMark - m_attackMark);
                m_followThroughDrag = followThroughSpeed / (m_recoverMark - m_followThroughMark);

                CrossFadeAnimation(m_attackMark);
            }

            public override void OnTick()
            {
                void ToAttack()
                {
                    m_state = State.Attack;
                    blackBoard.movement.SetVelocity(m_attackDirection * m_attackSpeed);
                    blackBoard.combat.SetWeaponState(0, true);
                }

                void ToFollowThrough()
                {
                    m_state = State.FollowThrough;
                    blackBoard.combat.RetractWeapons();
                }

                void ToRecover()
                {
                    m_state = State.Recover;
                    Debug.Log($"Switched to recovering state with a remaining velocity of {blackBoard.movement.velocity.magnitude}, and a remaining deceleration of {m_attackDrag * blackBoard.deltaTime}.");
                }

                m_timer += blackBoard.deltaTime;
                switch (m_state)
                {
                    case State.WindUp:
                        blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_brakeDrag);
                        blackBoard.transform.rotation = Quaternion.Slerp(m_startRotation, m_endRotation, m_timer / m_attackMark);
                        if (m_timer < m_attackMark) break;
                        ToAttack();
                        break;

                    case State.Attack:
                        blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_attackDrag);
                        if (m_timer < m_followThroughMark) break;
                        ToFollowThrough();
                        break;

                    case State.FollowThrough:
                        blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_followThroughDrag);
                        if (m_timer < m_recoverMark) break;
                        ToRecover();
                        break;

                    case State.Recover:
                        if (m_timer < m_endMark) break;
                        SwitchToState(m_returnState);
                        break;

                }
            }

            public override void OnExit()
            {
                m_attackDirection = Vector2.zero;
                m_returnState = null;

                m_state = State.WindUp;
                m_timer = 0f;

                m_brakeDrag = 0f;
                m_attackDrag = 0f;

                blackBoard.movement.SetVelocity(Vector2.zero);
            }

            private enum State { WindUp, Attack, FollowThrough, Recover }
        }
    }
}
