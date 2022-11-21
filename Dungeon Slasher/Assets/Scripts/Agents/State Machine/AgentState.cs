using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        public abstract class AgentState : State
        {
            /// <summary>
            /// The behavior handler helping with movement behaviors.
            /// </summary>
            protected BehaviorHandler behavior { get; private set; }

            /// <summary>
            /// The state machine this state is a part of.
            /// </summary>
            protected Blackboard blackBoard { get => (parent as AgentFSM).blackboard; }

            /// <summary>
            /// Called whenever the finite state machine the state is in, is created.
            /// </summary>
            public override void Initialize(FStateMachine parent)
            {
                base.Initialize(parent);
                this.behavior   = new BehaviorHandler();
            }
        }
    }
}