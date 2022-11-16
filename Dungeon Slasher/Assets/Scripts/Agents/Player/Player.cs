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
            m_stateMachine = new FiniteStateMachine(m_blackBoard, typeof(FreeMove), new FreeMove(), new Attack());
        }

        public override void Tick(float deltaTime)
        {
            Controls.CheckInput(m_blackBoard);
            base.Tick(deltaTime);
        }
    }
}
