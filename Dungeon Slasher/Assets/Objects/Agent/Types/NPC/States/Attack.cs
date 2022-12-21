using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonSlasher.Agents
{
    public partial class NPC
    {
        [System.Serializable]
        public class Attack : AttackState
        {
            protected override void ToFinish()
            {
                SwitchToState<ChasePlayer>();
            }
        }
    }
}
