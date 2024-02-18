namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class Condition
    {
        public readonly Predicate condition = null;
        public readonly System.Type state   = null;

        public Condition(Predicate _condition, System.Type _stateToSwitchTo)
        {
            condition   = _condition;
            state       = _stateToSwitchTo;
        }

        public delegate bool Predicate();
    }
}