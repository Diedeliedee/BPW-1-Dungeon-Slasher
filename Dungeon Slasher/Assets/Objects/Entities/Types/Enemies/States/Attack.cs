using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Enemy
{
    public class Attack : AttackState<Enemy>
    {
        public Attack(Enemy root, Settings settings) : base(root, settings) { }

        protected override void ToFinish()
        {
            SwitchToState(typeof(ChasePlayer));
        }
    }
}
