using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Player
{
    public class LeftAttack : Attack
    {
        public LeftAttack(Player root, Settings settings) : base(root, settings) { }

        protected override void DuringFollowThrough(float deltaTime)
        {
            base.DuringFollowThrough(deltaTime);
            if (m_buffer == null) return;

            var input = m_buffer.input;

            SwitchToState<RightAttack>().Setup(input);
        }
    }
}
