using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        public class Attack : State
        {
            //  Properties:
            private float m_attackTreshhold = 3f;   //  The maximum speed until the player can slash.
            private float m_brakeDrag = 75f;

            private float m_attackDrag = 200f;
            private float m_attackSpeed = 60f;

            //  Run-time:
            private int m_state = 0;
            private Vector2 m_attackDirection = Vector2.zero;

            public override void OnEnter()
            {
                m_attackDirection = Controls.rightInput.normalized;

                blackBoard.movement.ResetProperties();
                blackBoard.movement.drag = m_brakeDrag;
            }

            public override void OnTick()
            {
                blackBoard.movement.TickPhysics(blackBoard.deltaTime);
                switch (m_state)
                {
                    //  Braking phase.
                    case 0:
                        if (blackBoard.movement.velocity.magnitude > m_attackTreshhold) break;
                        blackBoard.movement.drag = m_attackDrag;
                        blackBoard.movement.SetVelocity(m_attackDirection * m_attackSpeed);
                        m_state = 1;
                        break;

                    case 1:
                        if (blackBoard.movement.velocity.magnitude > 0f) break;
                        parent.SwitchToState(typeof(FreeMove));
                        break;

                }
            }

            public override void OnExit()
            {
                m_attackDirection = Vector2.zero;
                m_state = 0;
            }

            public override void OnDrawGizmos()
            {
                var height = blackBoard.transform.position.y;
                var endPosition3D = Calc.FlatToVector(m_attackDirection, height);
            }
        }
    }
}
