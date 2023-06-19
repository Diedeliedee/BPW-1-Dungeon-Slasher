using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

public partial class Player
{
    public class Teleport : Ability<Player>
    {
        private float m_distance = 0f;

        public Teleport(Player root, float distance) : base(root)
        {
            m_distance = distance;
        }

        public override void ActivateAbility()
        {
            base.ActivateAbility();

            var direction = Vectors.FlatToVector(m_root.m_controls.rightInputWorldDir);

            m_root.movement.controller.Move(direction * m_distance);
        }

        public override void OnAbilityActive(float deltaTime)
        {
            m_active = false;
        }

        public override float GetCooldown()
        {
            //  This is why you would use ScriptableObjects..
            return 1f;
        }
    }
}