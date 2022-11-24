using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        [System.Serializable]
        public class Attack : AgentState
        {
            /// Velocity    = Time     * Drag
            /// Velocity    = Distance / Time
            /// Drag        = Velocity / Time
            /// Time        = Velocity / Drag
            /// Distance    = Velocity * Time (If the velocity would'nt have drag.)
            /// 

            //  Properties:
            [Space]
            [SerializeField] private float m_attackTreshhold = 3f;   //  The maximum speed until the player can slash.
            [SerializeField] private float m_brakeDrag = 75f;
            [Space]
            [SerializeField] private float m_attackDrag = 200f;
            [SerializeField] private float m_attackSpeed = 60f;

            //  Run-time:
            private int m_state = 0;
            private Vector2 m_attackDirection = Vector2.zero;
            private System.Type m_returnState = null;

            public void SetAttack(Vector2 direction, System.Type returnState)
            {
                m_attackDirection = direction.normalized;
                m_returnState = returnState;
            }

            public override void OnEnter()
            {
                CrossFadeAnimation();
            }

            public override void OnTick()
            {
                switch (m_state)
                {
                    //  Braking phase.
                    case 0:
                        blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_brakeDrag);

                        if (blackBoard.movement.velocity.magnitude > m_attackTreshhold) break;

                        blackBoard.movement.SetVelocity(m_attackDirection * m_attackSpeed);
                        blackBoard.combat.SetWeaponState(0, true);
                        m_state = 1;
                        break;

                    //  Attacking phase.
                    case 1:
                        blackBoard.movement.TickPhysics(blackBoard.deltaTime, m_attackDrag);

                        if (blackBoard.movement.velocity.magnitude > 0f) break;

                        parent.SwitchToState(m_returnState);
                        break;

                }
            }

            public override void OnExit()
            {
                m_state = 0;
                m_attackDirection = Vector2.zero;
                m_returnState = null;

                blackBoard.combat.RetractWeapons();
            }
        }
    }
}
