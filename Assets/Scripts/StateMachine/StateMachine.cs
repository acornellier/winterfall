using System;
using System.Collections.Generic;
using System.Linq;

public class StateMachine
{
    static readonly List<Transition> EmptyTransitions = new(0);

    readonly Dictionary<Type, List<Transition>> _transitions = new();

    IState _currentState;
    List<Transition> _currentTransitions = new();

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        _currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == _currentState)
            return;

        _currentState?.OnExit();
        _currentState = state;

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        _currentTransitions ??= EmptyTransitions;

        _currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out var matchingTransitions) == false)
        {
            matchingTransitions = new List<Transition>();
            _transitions[from.GetType()] = matchingTransitions;
        }

        matchingTransitions.Add(new Transition(to, predicate));
    }

    Transition GetTransition()
    {
        return _currentTransitions.FirstOrDefault(transition => transition.Condition());
    }

    class Transition
    {
        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

        public Func<bool> Condition { get; }
        public IState To { get; }
    }
}