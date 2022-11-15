using System.Collections.Generic;
using System.Threading.Tasks;

public class AgentFSM : FiniteStateMachine
{
    private Dictionary<System.Type, AgentState> m_states    = null;
    private AgentState m_currentState                       = null;

    public AgentFSM(System.Type startState, params AgentState[] states) : base(startState, states)
    {
        m_states = new Dictionary<System.Type, AgentState>();
        foreach (var state in states)
        {
            state.Initialize(this);
            state.OnStart();
            m_states.Add(state.GetType(), state);
        }
        SwitchToState(startState);
    }

    public virtual void SwitchToState(System.Type stateToSwitchTo, AgentContext context)
    {
        m_currentState?.OnExit(context);
        m_currentState = m_states[stateToSwitchTo];
        m_currentState?.OnEnter(context);
    }

    public virtual void Tick(AgentContext context)
    {
        m_currentState.OnTick(context);
    }

    public virtual void DrawGizmos(AgentContext context)
    {
        m_currentState?.OnDrawGizmos(context);
    }
}
