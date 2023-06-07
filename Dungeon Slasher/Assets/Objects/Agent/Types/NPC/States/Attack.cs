using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class NPC
{
    public class Attack : AttackState<NPC>
    {
        public Attack(NPC root, Settings settings) : base(root, settings) { }

        protected override void ToFinish()
        {
            SwitchToState(typeof(ChasePlayer));
        }
    }
}
