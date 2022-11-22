using Dodelie.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class NPC
    {
        [System.Serializable]
        public class ChaseAgent : MovementState
        {
            [Space]
            [SerializeField] private float m_lookAheadTime = 0.5f;
            [SerializeField] private Transform m_target = null;

            public override void OnStart()
            {
                SetBehaviors(new Pursue(m_lookAheadTime, m_target));
            }

            public override void OnTick()
            {
                TickMovement();
            }
        }
    }
}
