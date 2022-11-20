using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Player : Agent
    {
        [System.Serializable]
        public class FreeMove : State
        {
            //  Properties:
            [SerializeField] private float m_speed = 10f;
            [SerializeField] private float m_grip = 0.1f;

            public override void OnEnter()
            {
                blackBoard.movement.speed = m_speed;
                blackBoard.movement.grip = m_grip;
            }

            public override void OnTick()
            {
                if (Controls.slashButtonPressed)
                {
                    parent.SwitchToState(typeof(Attack));
                    return;
                }

                blackBoard.movement.MoveDirection(Controls.leftInput.normalized, blackBoard.deltaTime);
            }

            public override void OnExit()
            {
                blackBoard.movement.ResetProperties();
            }
        }
    }
}