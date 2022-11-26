using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        [System.Serializable]
        public abstract class AttackState : AgentState
        {
            /// Velocity    = Time          * Drag
            /// Drag        = Velocity      / Time
            /// Drag        = Velocity^2    / (2 * Distance)

            [Header("Attack Properties:")]
            [SerializeField] private float m_attackMark                             = 0.05f;
            [SerializeField] private float m_followThroughMark                      = 0.2f;
            [SerializeField] private float m_recoverMark                            = 0.45f;
            [SerializeField] private float m_endMark                                = 1f;
            [Space]
            [SerializeField] private float m_attackSpeed                            = 60f;
            [Range(0f, 1f)] [SerializeField] private float m_followThroughPercent   = 0.1f;

            //  Transformational data:
            private Vector2 m_attackDirection   = Vector2.zero;
            private Quaternion m_startRotation  = Quaternion.identity;
            private Quaternion m_endRotation    = Quaternion.identity;

            //  Kinetic properties:
            private float m_brakeDrag           = 0f;
            private float m_attackDrag          = 0f;
            private float m_followThroughDrag   = 0f;

            //  
            private Phase m_phase               = Phase.WindUp;
            private float m_timer               = 0f;

            /// <summary>
            /// Function unique to an attack state. Called in the same line as the switch to the state.
            /// </summary>
            /// <param name="direction"></param>
            public virtual void InitiateAttack(Vector2 direction)
            {
                var followThroughSpeed  = m_attackSpeed * m_followThroughPercent;

                m_attackDirection       = direction.normalized;
                m_startRotation         = blackBoard.transform.rotation;
                m_endRotation           = Quaternion.LookRotation(Calc.FlatToVector(m_attackDirection, blackBoard.transform.position.y));

                m_brakeDrag             = blackBoard.movement.velocity.magnitude / m_attackMark;
                m_attackDrag            = (m_attackSpeed - followThroughSpeed) / (m_followThroughMark - m_attackMark);
                m_followThroughDrag     = followThroughSpeed / (m_recoverMark - m_followThroughMark);

                CrossFadeAnimation(m_attackMark);
            }

            public override void OnTick()
            {
                void ExecuteOnExceed(float time, AttackEvent onExceed)
                {
                    if (m_timer < time) return;
                    onExceed.Invoke();
                }

                m_timer += blackBoard.deltaTime;
                switch (m_phase)
                {
                    case Phase.WindUp:
                        //  Brake to prepare for the attack, and turn in the right direction.
                        DuringBrake();
                        ExecuteOnExceed(m_attackMark, ToAttack);
                        break;

                    case Phase.Attack:
                        //  Slide swiftly to the attack direction.
                        DuringAttack();
                        ExecuteOnExceed(m_followThroughMark, ToFollowThrough);
                        break;

                    case Phase.FollowThrough:
                        //  Slide slowly attempting to brake again.
                        DuringFollowThrough();
                        ExecuteOnExceed(m_recoverMark, ToRecover);
                        break;

                    case Phase.Recover:
                        DuringRecover();
                        ExecuteOnExceed(m_endMark, ToFinish);
                        break;

                }
            }

            #region Updaters

            protected virtual void DuringBrake()
            {
                blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_brakeDrag);
                blackBoard.transform.rotation = Quaternion.Slerp(m_startRotation, m_endRotation, m_timer / m_attackMark);
            }

            protected virtual void DuringAttack()
            {
                blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_attackDrag);
            }

            protected virtual void DuringFollowThrough()
            {
                blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_followThroughDrag);
            }

            protected virtual void DuringRecover()
            {

            }

            #endregion


            #region Events

            /// <summary>
            /// Called when entering the attack phase.
            /// </summary>
            protected virtual void ToAttack()
            {
                m_phase = Phase.Attack;
                blackBoard.movement.SetVelocity(m_attackDirection * m_attackSpeed);
                blackBoard.combat.SetWeaponState(0, true);
            }

            /// <summary>
            /// Called when entering the follow-through phase.
            /// </summary>
            protected virtual void ToFollowThrough()
            {
                m_phase = Phase.FollowThrough;
                blackBoard.combat.RetractWeapons();
            }

            /// <summary>
            /// Called when entering the recovery phase.
            /// </summary>
            protected virtual void ToRecover()
            {
                m_phase = Phase.Recover;
                Debug.Log($"Switched to recovering state with a remaining velocity of {blackBoard.movement.velocity.magnitude}, and a remaining deceleration of {m_attackDrag * blackBoard.deltaTime}.");
            }

            /// <summary>
            /// Called when the state is ending.
            /// </summary>
            protected abstract void ToFinish();

            #endregion

            public override void OnExit()
            {
                m_attackDirection   = Vector2.zero;
                m_startRotation     = Quaternion.identity;
                m_endRotation       = Quaternion.identity;

                m_brakeDrag         = 0f;
                m_attackDrag        = 0f;
                m_followThroughDrag = 0f;

                m_phase             = Phase.WindUp;
                m_timer             = 0f;

                blackBoard.movement.SetVelocity(Vector2.zero);
            }

            private enum Phase { WindUp, Attack, FollowThrough, Recover }
            private delegate void AttackEvent();
        }
    }
}
