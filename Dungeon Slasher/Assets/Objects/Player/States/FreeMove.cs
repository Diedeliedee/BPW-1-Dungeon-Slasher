using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;

public partial class Player
{
    public class FreeMove : Execution<Player>
    {
        public override void OnEnter()
        {
            source.m_movement.speed = source.configuration.speed;
            source.m_movement.grip  = source.settings.grip;
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