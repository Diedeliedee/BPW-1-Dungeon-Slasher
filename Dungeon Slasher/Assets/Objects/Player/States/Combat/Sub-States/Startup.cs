using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player
{
    public class Startup : Execution<Player>
    {
        protected readonly string m_animation = null;

        private float m_startAngle  = default;
        private float m_targetAngle = default;


        public Startup(string _animationName)
        {
            m_animation = _animationName;
        }

        public override void OnEnter()
        {
            m_startAngle    = source.angle;
            m_targetAngle   = source.m_input.lookInput.Rotate(m_offsetAngle).normalized.ToAngle();

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