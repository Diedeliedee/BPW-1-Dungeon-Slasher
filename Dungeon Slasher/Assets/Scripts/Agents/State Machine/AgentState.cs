using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        [System.Serializable]
        public abstract class AgentState : State
        {
            [SerializeField] private AnimationClip m_animation = null;

            /// <summary>
            /// The state machine this state is a part of.
            /// </summary>
            protected Blackboard blackBoard { get => (parent as AgentFSM).blackboard; }

            protected void CrossFadeAnimation(float seconds = 0f)
            {
                if (m_animation == null) return;
                blackBoard.animator.CrossFadeInFixedTime(m_animation.name, seconds);
            }
        }
    }
}