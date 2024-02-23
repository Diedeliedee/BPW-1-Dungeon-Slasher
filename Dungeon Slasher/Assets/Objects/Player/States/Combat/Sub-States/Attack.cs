using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player
{
    public class Attack : CombatState
    {
        public Attack(string _animationName) : base(_animationName) { }

        public override void OnEnter()
        {
            source.m_movement.velocity = source.forward * source.m_attackThing1;
            source.m_movement.drag = Mathf.Abs(Util.CalculateDeceleration(source.m_attackThing1, 0f, source.m_attackThing2));

            source.m_animator.Play(m_animation, -1, 0f);
        }

        public override void OnTick()
        {
            source.m_movement.IterateInDirection();
        }
    }
}