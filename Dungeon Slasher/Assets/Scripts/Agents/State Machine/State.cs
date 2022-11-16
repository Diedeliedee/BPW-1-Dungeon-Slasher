using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonSlasher.Agents
{
    public abstract partial class State
    {
        private FSM m_parent = null;

        /// <summary>
        /// The state machine this state is a part of.
        /// </summary>
        protected FSM parent { get => m_parent; }

        public State(Agent root, FSM parent)
        {

            m_parent = parent;
        }

        /// <summary>
        /// Called whenever the finite state machine the state is in, is created.
        /// </summary>
        public void Initialize(FSM parent)
        {
            m_parent = parent;
        }

        public virtual void OnTick(Context context)         { }

        public virtual void OnEnter(Context context)        { }

        public virtual void OnExit(Context context)         { }

        public virtual void OnDrawGizmos(Context context)   { }
    }
}
