using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class NPC : Agent
    {
        public override void Initialize()
        {
            base.Initialize();
            m_stateMachine = new FiniteStateMachine(m_blackBoard, typeof(Idle), new Idle());
        }
    }
}
