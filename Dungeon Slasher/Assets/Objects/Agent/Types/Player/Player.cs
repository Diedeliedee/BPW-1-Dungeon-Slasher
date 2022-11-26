using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        [Header("Player States:")]
        [SerializeField] private FreeMove m_freeMove        = null;
        [SerializeField] private LeftAttack m_leftAttack    = null;
        [SerializeField] private RightAttack m_rightAttack  = null;

        public override void Initialize()
        {
            base.Initialize();

            SetStates(typeof(FreeMove), m_freeMove, m_leftAttack, m_rightAttack);
        }

        public override void Tick(float deltaTime)
        {
            PlayerControls.CheckInput(m_blackBoard);

            base.Tick(deltaTime);
        }
    }
}
