using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        [System.Serializable]
        public class PlayerAttack : AttackState
        {
            protected AttackBuffer m_buffer = null;

            public override void InitiateAttack(Vector2 direction)
            {
                direction = Calc.RotateVector2(direction, PlayerControls.inputRotation);
                base.InitiateAttack(direction);
            }

            protected override void DuringAttack()
            {
                base.DuringAttack();
                BufferOnAttackInput();
            }

            protected override void DuringFollowThrough()
            {
                base.DuringFollowThrough();
                if (BufferOnAttackInput() || m_buffer != null) return;
                if (Controls.activeLeftInput)
                {
                    SwitchToState<FreeMove>();
                    return;
                }
            }


            protected override void ToFinish()
            {
                SwitchToState<FreeMove>();
            }

            public override void OnExit()
            {
                base.OnExit();
                m_buffer = null;
            }

            /// <summary>
            /// If the slash button is pressed, an attack buffer instance will be created.
            /// </summary>
            /// <returns>True if the slash button is pressed, false if not.</returns>
            private bool BufferOnAttackInput()
            {
                if (!PlayerControls.slashButtonPressed) return false;
                m_buffer = new AttackBuffer(Controls.rightInput);
                return true;
            }

            /// <returns>True if the new attack direction is greater than 90 degrees from the current attack direction.</returns>
            protected bool BackwardAttack()
            {
                if (m_buffer == null)
                {
                    Debug.LogError("Cannot compare new attack direction if the attack buffer is non-existent.");
                    return false;
                }
                return Vector2.Angle(m_buffer.input, m_attackDirection) > 90f;
            }

            protected class AttackBuffer
            {
                public readonly Vector2 input;

                public AttackBuffer(Vector2 input)
                {
                    this.input = input;
                }
            }
        }
    }
}