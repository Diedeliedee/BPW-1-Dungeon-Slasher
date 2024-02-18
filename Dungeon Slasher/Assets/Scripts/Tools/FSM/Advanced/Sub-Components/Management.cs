﻿using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    internal class Management<T> : IStateMachine
    {
        public Dictionary<System.Type, CompositeState<T>> children { get; private set; }

        //  Run-time:
        private CompositeState<T> m_activeChild     = null;
        private CompositeState<T> m_defaultChild    = null;

        public Management(params CompositeState<T>[] _children)
        {
            children = new Dictionary<System.Type, CompositeState<T>>();
            foreach (var _child in _children)
            {
                children.Add(_child.GetType(), _child);
            }
            m_defaultChild = _children[0];
        }

        public void Tick()
        {
            m_activeChild?.Tick();
        }

        public void OnSwitch(System.Type state)
        {
            m_activeChild?.Reset();
            m_activeChild?.OnExit();

            try { m_activeChild = children[state]; }
            catch { Debug.LogError($"The child state: '{state.Name}' is not found within the state's lower hierarchy."); return; }

            m_activeChild.OnEnter();
            m_activeChild.Activate();
            Debug.Log($"Entered state: '{state.Name}'.");
        }

        public void Activate()
        {
            m_activeChild = m_defaultChild;
            m_activeChild.OnEnter();
            m_activeChild.Activate();
        }

        public void Reset()
        {
            m_activeChild.Reset();
            m_activeChild.OnExit();
            m_activeChild = null;
        }
    }
}