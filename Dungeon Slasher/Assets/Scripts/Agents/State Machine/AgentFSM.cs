using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        public class AgentFSM : FStateMachine
        {
            /// <summary>
            /// Blackboard containing information regarding the agent. Gets updated before the state machine gets ticked.
            /// </summary>
            public Blackboard blackboard { get; private set; }

            public AgentFSM(Blackboard blackboard, System.Type startState, params AgentState[] states) : base(startState, states)
            {
                this.blackboard = blackboard;
                SwitchToState(startState);
            }
        }
    }
}