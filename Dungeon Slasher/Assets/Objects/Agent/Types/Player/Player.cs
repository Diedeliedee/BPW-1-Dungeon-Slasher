using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        [Header("Player States:")]
        [SerializeField] private FreeMove m_freeMove    = null;
        [SerializeField] private AttackState m_attack   = null;

        private PlayerControls m_controls               = null;

        public override void Initialize()
        {
            base.Initialize();

            m_controls              = new PlayerControls();
            m_freeMove.controls     = m_controls;

            SetStates(typeof(FreeMove), m_freeMove, m_attack);
        }

        public override void Tick(float deltaTime)
        {
            m_controls.CheckInput(m_blackBoard);

            base.Tick(deltaTime);
        }
    }
}
