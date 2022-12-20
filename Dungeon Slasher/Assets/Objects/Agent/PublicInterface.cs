using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public Health health { get => m_health; }
        public Movement movement { get => m_movement; }
        public Combat combat { get => m_combat; }
        public Animator animator { get => m_animator; }

        public Collider collider { get => m_movement.collider; }

        public Vector2 flatPosition { get => new Vector2(transform.position.x, transform.position.z); set => transform.position = new Vector3(value.x, transform.position.y, value.y); }

        public void ChangeHealth(int amount)
        {
            m_health.AddHealth(amount);
        }
    }
}
