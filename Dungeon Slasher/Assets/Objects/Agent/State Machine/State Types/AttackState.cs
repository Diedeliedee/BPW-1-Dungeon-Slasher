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
            [SerializeField] private float m_attackMark = 0.05f;
            [SerializeField] private float m_followThroughMark = 0.2f;
            [SerializeField] private float m_recoverMark = 0.45f;
            [SerializeField] private float m_endMark = 1f;
            [Space]
            [SerializeField] private float m_attackSpeed = 60f;
            [Range(0f, 1f)] [SerializeField] private float m_followThroughPercent = 0.1f;

            //  Transformational data:
            protected Vector2 m_attackDirection = Vector2.zero;
            private Quaternion m_startRotation = Quaternion.identity;
            private Quaternion m_endRotation = Quaternion.identity;

            //  Kinetic properties:
            private float m_brakeDrag = 0f;
            private float m_attackDrag = 0f;
            private float m_followThroughDrag = 0f;

            //  
            private Phase m_phase = Phase.WindUp;
            private float m_timer = 0f;

            /// <summary>
            /// Function unique to an attack state. Called in the same line as the switch to the state.
            /// </summary>
            /// <param name="direction"></param>
            public virtual void InitiateAttack(Vector2 direction)
            {
                var followThroughSpeed = m_attackSpeed * m_followThroughPercent;

                m_attackDirection = direction.normalized;
                m_startRotation = root.transform.rotation;
                m_endRotation = Quaternion.LookRotation(Calc.FlatToVector(m_attackDirection, root.transform.position.y));

                m_brakeDrag = root.movement.velocity.magnitude / m_attackMark;
                m_attackDrag = (m_attackSpeed - followThroughSpeed) / (m_followThroughMark - m_attackMark);
                m_followThroughDrag = followThroughSpeed / (m_recoverMark - m_followThroughMark);

                CrossFadeAnimation(m_attackMark);
            }

            public override void OnTick(float deltaTime)
            {
                void ExecuteOnExceed(float time, AttackEvent onExceed)
                {
                    if (m_timer < time) return;
                    onExceed.Invoke();
                }

                m_timer += deltaTime;
                switch (m_phase)
                {
                    case Phase.WindUp:
                        //  Brake to prepare for the attack, and turn in the right direction.
                        DuringBrake(deltaTime);
                        ExecuteOnExceed(m_attackMark, ToAttack);
                        break;

                    case Phase.Attack:
                        //  Slide swiftly to the attack direction.
                        DuringAttack(deltaTime);
                        ExecuteOnExceed(m_followThroughMark, ToFollowThrough);
                        break;

                    case Phase.FollowThrough:
                        //  Slide slowly attempting to brake again.
                        DuringFollowThrough(deltaTime);
                        ExecuteOnExceed(m_recoverMark, ToFinish);
                        Debug.Log($"Switched to recovering state with a remaining velocity of {root.movement.velocity.magnitude}, and a remaining deceleration of {m_attackDrag * deltaTime}.");
                        break;

                }
            }

            #region Updaters

            protected virtual void DuringBrake(float deltaTime)
            {
                root.movement.TickPhysics(deltaTime, m_brakeDrag);
                root.transform.rotation = Quaternion.Slerp(m_startRotation, m_endRotation, m_timer / m_attackMark);
            }

            protected virtual void DuringAttack(float deltaTime)
            {
                root.movement.TickPhysics(deltaTime, m_attackDrag);
            }

            protected virtual void DuringFollowThrough(float deltaTime)
            {
                root.movement.TickPhysics(deltaTime, m_followThroughDrag);
            }

            #endregion

            #region Events

            /// <summary>
            /// Called when entering the attack phase.
            /// </summary>
            protected virtual void ToAttack()
            {
                m_phase = Phase.Attack;
                root.movement.SetVelocity(m_attackDirection * m_attackSpeed);
                root.combat.SetWeaponState(0, true);
            }

            /// <summary>
            /// Called when entering the follow-through phase.
            /// </summary>
            protected virtual void ToFollowThrough()
            {
                m_phase = Phase.FollowThrough;
                root.combat.RetractWeapons();
            }

            /// <summary>
            /// Called when the state is ending.
            /// </summary>
            protected abstract void ToFinish();

            #endregion

            public override void OnExit()
            {
                m_attackDirection = Vector2.zero;
                m_startRotation = Quaternion.identity;
                m_endRotation = Quaternion.identity;

                m_brakeDrag = 0f;
                m_attackDrag = 0f;
                m_followThroughDrag = 0f;

                m_phase = Phase.WindUp;
                m_timer = 0f;

                root.movement.SetVelocity(Vector2.zero);
            }

            private enum Phase { WindUp, Attack, FollowThrough }
            private delegate void AttackEvent();
        }
    }
}
