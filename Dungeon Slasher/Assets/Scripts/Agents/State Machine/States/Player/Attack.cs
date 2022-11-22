using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        [System.Serializable]
        public class Attack : AgentState
        {
            //  Properties:
            [SerializeField] private float m_attackTreshhold = 3f;   //  The maximum speed until the player can slash.
            [SerializeField] private float m_brakeDrag = 75f;
            [Space]
            [SerializeField] private float m_attackDrag = 200f;
            [SerializeField] private float m_attackSpeed = 60f;

            //  Run-time:
            private int m_state = 0;
            private Vector2 m_attackDirection = Vector2.zero;

            public void SetAttackDirection(Vector2 direction)
            {
                m_attackDirection = direction;
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

                        parent.SwitchToState<FreeMove>();
                        break;

                }
            }

            public override void OnExit()
            {
                m_attackDirection = Vector2.zero;
                m_state = 0;
                blackBoard.combat.RetractWeapons();
            }
        }
    }
}