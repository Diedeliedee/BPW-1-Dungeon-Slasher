using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    public abstract class CombatState : Execution<Player>
    {
        protected readonly string m_animation = null;

        public CombatState(string _animationName)
        {
            m_animation = _animationName;
        }
    }
}