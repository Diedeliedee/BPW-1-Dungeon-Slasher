using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public class BehaviorHandler
    {
        private Behavior[] m_behaviors = null;

        public void SetBehaviors(params Behavior[] behaviors)
        {
            m_behaviors = behaviors;
        }

        public Vector2 GetDesiredVelocity(Vector2 position, float speed)
        {
            var desiredVelocity = Vector2.zero;

            for (int i = 0; i < m_behaviors.Length; i++)
            {
                desiredVelocity += m_behaviors[i].GetDesiredVelocity(position, speed);
            }
            desiredVelocity = Vector2.ClampMagnitude(desiredVelocity, speed);
            return desiredVelocity;
        }

        public void DrawGizmos(Vector3 position)
        {
            for (int i = 0; i < m_behaviors.Length; i++) m_behaviors[i].DrawGizmos(position);
        }
    }
}
