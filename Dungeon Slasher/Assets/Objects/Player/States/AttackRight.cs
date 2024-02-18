using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player
{
    public class AttackRight : Execution<Player>
    {
        private const float m_offsetAngle = 45f;

        public override void OnEnter()
        {
            var attackDir = source.m_input.lookInput.Rotate(m_offsetAngle).normalized;

            source.forward = attackDir;
            source.m_movement.velocity = attackDir * source.m_attackThing1;
            source.m_movement.drag = Mathf.Abs(Util.CalculateDeceleration(source.m_attackThing1, 0f, source.m_attackThing2));
            source.m_animator.Play(source.m_attackRightName, -1, 0f);
        }

        public override void OnTick()
        {
            source.m_movement.IterateInDirection();
        }
    }
}