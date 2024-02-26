using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player
{
    public class Attack : Execution<Player>
    {
        private readonly string m_attack;
        private readonly string m_recovery;

        public Attack(string _attackAnimation, string _recoverAnimation)
        {
            m_attack = _attackAnimation;
            m_recovery = _recoverAnimation;
        }

        public override void OnEnter()
        {
            source.m_combat.StartAttack();

            source.m_movement.velocity  = source.forward * source.m_attackThing1;
            source.m_movement.drag      = Mathf.Abs(Util.CalculateDeceleration(source.m_attackThing1, 0f, source.m_attackThing2));

            source.m_animator.Play(m_attack, -1, 0f);
        }

        public override void OnTick()
        {
            source.m_movement.IterateInDirection();
        }

        public override void OnExit()
        {
            source.m_combat.EndAttack();
            source.m_animator.Play(m_recovery, -1, 0f);
        }
    }
}