using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    public class FreeMove : Execution<Player>
    {
        public override void OnEnter()
        {
            source.m_movement.speed = source.m_speed;
            source.m_movement.grip = source.m_grip;
        }

        public override void OnTick()
        {
            source.m_movement.MoveInDirection(source.m_input.moveInput);
            source.m_rotation.RotateTo(source.m_input.lookInput);
        }
    }
}