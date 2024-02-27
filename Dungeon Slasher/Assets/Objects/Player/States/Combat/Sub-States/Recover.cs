using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;

public partial class Player
{
    public class Recover : Execution<Player>
    {
        protected readonly string m_animation = null;

        public Recover(string _animationName)
        {
            m_animation = _animationName;
        }

        public override void OnEnter()
        {
            source.m_movement.speed = source.m_speed * source.m_recoverSpeedMultiplier;
            source.m_movement.grip = source.m_grip;

            source.m_animator.Play(m_animation, -1, 0f);
        }

        public override void OnTick()
        {
            var movementDirection = source.m_input.moveInput.Rotate(m_offsetAngle);
            var lookDirection = source.m_input.lookInput.Rotate(m_offsetAngle);

            source.m_movement.MoveInDirection(movementDirection);
            source.m_rotation.RotateTo(lookDirection);
        }
    }
}