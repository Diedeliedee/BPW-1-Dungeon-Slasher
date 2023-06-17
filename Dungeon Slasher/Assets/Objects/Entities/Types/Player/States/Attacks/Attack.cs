using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;

public partial class Player
{
    public class Attack : AttackState<Player>
    {
        protected AttackBuffer m_buffer = null;

        public Attack(Player root, Settings settings) : base(root, settings) { }

        public override void Setup(Vector2 direction)
        {
            direction = root.m_controls.rightInputWorldDir;
            base.Setup(direction);
        }

        protected override void DuringAttack(float deltaTime)
        {
            base.DuringAttack(deltaTime);
            BufferOnAttackInput();
        }

        protected override void DuringFollowThrough(float deltaTime)
        {
            base.DuringFollowThrough(deltaTime);
            if (BufferOnAttackInput() || m_buffer != null) return;
        }

        protected override void ToFinish()
        {
            SwitchToState(typeof(FreeMove));
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
            if (!root.m_controls.slashButtonPressed) return false;
            m_buffer = new AttackBuffer(root.m_controls.rightInput);
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