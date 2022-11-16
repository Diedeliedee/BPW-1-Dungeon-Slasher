using System.Collections.Generic;
using System.Threading.Tasks;

namespace DungeonSlasher.Agents
{
    public class FSM
    {
        public readonly Agent root = null;

        private readonly Dictionary<System.Type, State> m_states = null;

        private State m_currentState = null;

        public FSM(Agent root, System.Type startState, params State[] states)
        {
            this.root = root;

            m_states = new Dictionary<System.Type, State>();
            foreach (var state in states)
            {
                state.Initialize(this);
                m_states.Add(state.GetType(), state);
            }

            SwitchToState(startState);
        }

        public virtual void SwitchToState(System.Type stateToSwitchTo)
        {

            m_currentState?.OnExit();
            m_currentState = m_states[stateToSwitchTo];
            m_currentState?.OnEnter();
        }

        public virtual void Tick(State.Context context)
        {
            m_currentState.OnTick(context);
        }

        public virtual void DrawGizmos(State.Context context)
        {
            m_currentState?.OnDrawGizmos(context);
        }
    }
}
