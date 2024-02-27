using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player
{
    public class Attack : Execution<Player>
    {
        protected readonly string m_animation = null;

        public Attack(string _animationName)
        {
            m_animation = _animationName;
        }

        public override void OnEnter()
        {
            source.m_combat.StartAttack();

            source.m_movement.velocity  = source.forward * source.m_attackVelocity;
            source.m_movement.drag      = Mathf.Abs(Util.CalculateDeceleration(source.m_attackVelocity, 0f, source.m_attackTime));

            source.m_animator.Play(m_animation, -1, 0f);
        }

        public override void OnTick()
        {
            source.m_movement.IterateInDirection();
        }

        public override void OnExit()
        {
            source.m_combat.EndAttack();
        }
    }
}