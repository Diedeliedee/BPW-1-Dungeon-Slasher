using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class NPC : Agent
    {
        [Header("NPC Properties:")]
        [SerializeField] private NPCConcept.Type m_type     = 0;

        [Header("NPC States:")]
        [SerializeField] private ChasePlayer m_chaseAgent   = null;
        [SerializeField] private Attack m_attack            = null;

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            SetStates(m_chaseAgent.GetType(), m_chaseAgent, m_attack, new Hitpause(), new Hitstun(m_chaseAgent.GetType()));
        }

        public override void Hit(int damage, Agent source, out System.Action onRetract)
        {
            GameManager.instance.events.onEnemyHit.Invoke();
            SwitchToState<Hitpause>().Initiate(damage, Calc.ToDirection(source.flatPosition, flatPosition), out onRetract);
        }

        public override void OnDeath()
        {
            GameManager.instance.agents.TakeBack(m_type, this);
        }
    }
}
