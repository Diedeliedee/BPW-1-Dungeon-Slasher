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
        public class RightAttack : Attack
        {
            protected override void DuringFollowThrough(float deltaTime)
            {
                base.DuringFollowThrough(deltaTime);
                if (m_buffer == null) return;

                var input = m_buffer.input;

                if (BackwardAttack())
                {
                    SwitchToState<RightAttack>().InitiateAttack(input);
                    return;
                }
                SwitchToState<LeftAttack>().InitiateAttack(input);
            }
        }
    }
    
}
