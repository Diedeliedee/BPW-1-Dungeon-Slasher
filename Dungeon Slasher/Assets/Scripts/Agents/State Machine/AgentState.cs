using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class AgentState : State
{
    public virtual void OnStart(AgentContext context)       { }

    public virtual void OnTick(AgentContext context)        { }

    public virtual void OnEnter(AgentContext context)       { }

    public virtual void OnExit(AgentContext context)        { }

    public virtual void OnDrawGizmos(AgentContext context)  { }
}
