using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;

public partial class Agent
{
    public abstract class AttackState<T> : FlexState<T> where T : Agent
    {
        //  Transformational data:
        protected Vector2 m_attackDirection = Vector2.zero;
        private Quaternion m_startRotation = Quaternion.identity;
        private Quaternion m_endRotation = Quaternion.identity;

        //  Kinetic properties:
        private float m_brakeDrag = 0f;
        private float m_attackDrag = 0f;
        private float m_followThroughDrag = 0f;

        //  State management:
        private Phase m_phase = Phase.WindUp;
        private float m_timer = 0f;

        public Settings settings { get => GetSettings<Settings>(); }

        public AttackState(T root, Settings settings) : base(root, settings) { }

        /// <summary>
        /// Function unique to an attack state. Called in the same line as the switch to the state.
        /// </summary>
        public virtual void Setup(Vector2 direction)
        {
            var followThroughSpeed = settings.attackSpeed * settings.followThroughPercent;

            m_attackDirection = direction.normalized;
            m_startRotation = root.transform.rotation;
            m_endRotation = Quaternion.LookRotation(Vectors.FlatToVector(m_attackDirection, root.transform.position.y));

            m_brakeDrag = root.m_movement.flatVelocity.magnitude / settings.attackMark;
            m_attackDrag = (settings.attackSpeed - followThroughSpeed) / (settings.followThroughMark - settings.attackMark);
            m_followThroughDrag = followThroughSpeed / (settings.recoverMark - settings.followThroughMark);

            root.CrossFadeAnimation(settings.animation, settings.attackMark);
        }

        public override void OnTick(float deltaTime)
        {
            void ExecuteOnExceed(float time, System.Action onExceed)
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
                    ExecuteOnExceed(settings.attackMark, ToAttack);
                    break;

                case Phase.Attack:
                    //  Slide swiftly to the attack direction.
                    DuringAttack(deltaTime);
                    ExecuteOnExceed(settings.followThroughMark, ToFollowThrough);
                    break;

                case Phase.FollowThrough:
                    //  Slide slowly attempting to brake again.
                    DuringFollowThrough(deltaTime);
                    ExecuteOnExceed(settings.recoverMark, ToFinish);
                    Debug.Log($"Switched to recovering state with a remaining velocity of {root.m_movement.velocity.magnitude}, and a remaining deceleration of {m_attackDrag * deltaTime}.");
                    break;

            }
        }

        #region Updaters

        protected virtual void DuringBrake(float deltaTime)
        {
            root.m_movement.ApplyDrag(m_brakeDrag, deltaTime);
            root.transform.rotation = Quaternion.Slerp(m_startRotation, m_endRotation, m_timer / settings.attackMark);
        }

        protected virtual void DuringAttack(float deltaTime)
        {
            root.m_movement.ApplyDrag(m_attackDrag, deltaTime);
            root.m_combat.TickWeapon();
        }

        protected virtual void DuringFollowThrough(float deltaTime)
        {
            root.m_movement.ApplyDrag(m_followThroughDrag, deltaTime);
        }

        #endregion

        #region Events

        /// <summary>
        /// Called when entering the attack phase.
        /// </summary>
        protected virtual void ToAttack()
        {
            m_phase = Phase.Attack;
            root.m_movement.flatVelocity = m_attackDirection * settings.attackSpeed;
            root.m_combat.ActivateWeapon();
        }

        /// <summary>
        /// Called when entering the follow-through phase.
        /// </summary>
        protected virtual void ToFollowThrough()
        {
            m_phase = Phase.FollowThrough;
            root.m_combat.DeactivateWeapon();
        }

        /// <summary>
        /// Called when the state is ending.
        /// </summary>
        protected virtual void ToFinish() { }

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

            root.m_movement.flatVelocity = Vector2.zero;
        }

        private enum Phase { WindUp, Attack, FollowThrough }

        [System.Serializable]
        public class Settings : FlexState<T>.Settings
        {
            /// Velocity    = Time          * Drag
            /// Drag        = Velocity      / Time
            /// Drag        = Velocity^2    / (2 * Distance)
           
            public AnimationClip animation;
            [Space]
            public float attackMark                             = 0.05f;
            public float followThroughMark                      = 0.2f;
            public float recoverMark                            = 0.45f;
            public float endMark                                = 1f;
            [Space]
            public float attackSpeed                            = 60f;
            [Range(0f, 1f)] public float followThroughPercent   = 0.1f;
        }
    }
}
