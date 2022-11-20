using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        [Header("Player States:")]
        [SerializeField] private FreeMove m_freeMove    = null;
        [SerializeField] private Attack m_attack        = null;

        public override void Initialize()
        {
            base.Initialize();
            m_stateMachine = new FiniteStateMachine(m_blackBoard, typeof(FreeMove), m_freeMove, m_attack);
        }

        public override void Tick(float deltaTime)
        {
            Controls.CheckInput(m_blackBoard);
            base.Tick(deltaTime);
        }
    }
}
