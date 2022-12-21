using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

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

            SetStates(m_freeMove.GetType(), m_freeMove, m_leftAttack, m_rightAttack, new Hitstun(m_freeMove.GetType()));
        }

        public override void Tick(float deltaTime)
        {
            PlayerControls.CheckInput(transform.position);

            base.Tick(deltaTime);
        }

        public override void Hit(int damage, Agent source, out System.Action onRetract)
        {
            onRetract = null;

            GameManager.instance.events.onPlayerHit.Invoke();
            SwitchToState<Hitstun>().Initiate(damage, Calc.ToDirection(source.flatPosition, flatPosition));
        }

        public override void OnDeath()
        {

        }
    }
}
