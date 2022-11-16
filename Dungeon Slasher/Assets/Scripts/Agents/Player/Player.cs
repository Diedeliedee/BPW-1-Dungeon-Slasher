using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        [SerializeField] private PlayerSettings m_settings = null;

        public override void Initialize()
        {
            base.Initialize();
            m_stateMachine = new FiniteStateMachine(m_blackBoard, typeof(FreeMove), new FreeMove(m_settings.freeMove), new Attack(m_settings.attack));
        }

        public override void Tick(float deltaTime)
        {
            Controls.CheckInput(m_blackBoard);
            base.Tick(deltaTime);
        }
    }
}
