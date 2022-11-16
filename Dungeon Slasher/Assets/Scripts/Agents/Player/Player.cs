using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public override void Initialize()
        {
            base.Initialize();
            m_stateMachine = new FSM(typeof(Movement), new Movement(), new Attack());
        }
    }
}
