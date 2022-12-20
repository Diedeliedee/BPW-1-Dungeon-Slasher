using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        [System.Serializable]
        public abstract class AgentState : State<Agent>
        {
            [Header("State Properties:")]
            [SerializeField] private AnimationClip m_animation = null;

            protected void CrossFadeAnimation(float seconds = 0f)
            {
                if (m_animation == null) return;
                root.animator.CrossFadeInFixedTime(m_animation.name, seconds);
            }
        }
    }
}