public partial class Player
{
    public class FollowThrough : CombatState
    {
        public FollowThrough(string _animationName) : base(_animationName) { }

        public override void OnEnter()
        {
            source.m_animator.Play(m_animation, -1, 0f);
        }

        public override void OnTick()
        {
            source.m_movement.IterateInDirection();
        }
    }
}