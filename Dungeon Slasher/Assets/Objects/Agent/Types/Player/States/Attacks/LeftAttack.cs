using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        [System.Serializable]
        public class LeftAttack : PlayerAttack
        {
            protected override void DuringFollowThrough()
            {
                base.DuringFollowThrough();
                if (m_buffer == null) return;

                var input = m_buffer.input;

                if (BackwardAttack())
                {
                    SwitchToState<LeftAttack>().InitiateAttack(input);
                    return;
                }
                SwitchToState<RightAttack>().InitiateAttack(input);
            }
        }
    }
    
}
