namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class CompositeState<T> : IState, IStateMachine
    {
        public readonly IStateMachine parent = null;

        private Execution<T> m_execution    = null;
        private Conditions m_conditions     = null;
        private Management<T> m_management  = null;

        /// <summary>
        /// Create a composite state with not branching states.
        /// </summary>
        public CompositeState(Execution<T> _execution, Conditions _conditions, IStateMachine _parent, T _source)
        {
            m_execution     = _execution;
            m_conditions    = _conditions;

            parent              = _parent;
            m_execution.source  = _source;
        }

        /// <summary>
        /// Create a composite state with branching states.
        /// </summary>
        public CompositeState(Execution<T> _execution, Conditions _conditions, IStateMachine _parent, T _source, CompositeState<T>[] _children)
        {
            m_execution     = _execution;
            m_conditions    = _conditions;
            m_management    = new Management<T>(_children);

            parent              = _parent;
            m_execution.source  = _source;
        }

        #region FSM Functions
        public void Tick()
        {
            if (m_conditions.GetStateToSwitchTo(out System.Type _state))
            {
                parent.OnSwitch(_state);
                return;
            }

            OnTick();
            m_management?.Tick();
        }

        public void OnSwitch(System.Type state)
        {
            m_management.OnSwitch(state);
        }
        #endregion

        #region State Functions
        public virtual void OnEnter() => m_execution.OnEnter();

        public virtual void OnTick() => m_execution.OnTick();

        public virtual void OnExit() => m_execution.OnExit();
        #endregion

        #region Composite Functions
        /// <summary>
        /// Activates this state's FSM to it's default state.
        /// </summary>
        public void Activate() => m_management?.Activate();

        /// <summary>
        /// Resets this state's FSM to it's default state.
        /// </summary>
        public void Reset() => m_management?.Reset();
        #endregion
    }
}