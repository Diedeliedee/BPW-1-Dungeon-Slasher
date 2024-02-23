using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player
{
    public class Startup : CombatState
    {
        private const float m_offsetAngle = 45f;

        private float m_startAngle  = default;
        private float m_targetAngle = default;

        public Startup(string _animationName) : base(_animationName) { }

        public override void OnEnter()
        {
            var attackDir = source.m_input.lookInput.Rotate(m_offsetAngle).normalized;

            m_startAngle = source.angle;
            m_targetAngle = attackDir.ToAngle();

            source.m_animator.Play(m_animation, -1, 0f);
        }

        public override void OnTick()
        {
            source.angle = Mathf.LerpAngle(m_startAngle, m_targetAngle, source.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        public override void OnExit()
        {
            m_startAngle = default;
            m_targetAngle = default;
        }
    }
}