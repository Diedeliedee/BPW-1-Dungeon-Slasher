using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;

public partial class Player
{
    [System.Serializable]
    public class AbilityHandler
    {
        [SerializeField] private KeyCode m_abilityKey = KeyCode.LeftShift;

        private Ability<Player> m_activeAbility = null;
        private Timer m_cooldown = null;

        public void SetAbility(Ability<Player> ability)
        {
            m_activeAbility = ability;
            m_cooldown = new Timer(ability.GetCooldown());
            m_cooldown.timer = ability.GetCooldown();
        }

        public void Tick(float deltaTime)
        {
            if (m_activeAbility == null) return;
            if (m_activeAbility.active)
            {
                m_activeAbility.OnAbilityActive(deltaTime);
            }
            else
            {
                if (!m_cooldown.HasReached(deltaTime) || !Input.GetKeyDown(m_abilityKey)) return;
                m_activeAbility.ActivateAbility();
                m_cooldown.time = m_activeAbility.GetCooldown();
                m_cooldown.Reset();
            }
        }
    }
}