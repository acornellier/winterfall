using System;
using System.Collections.Generic;
using System.Linq;

public class StateMachine<T> where T : IState
{
    static readonly List<Transition> EmptyTransitions = new(0);

    readonly Dictionary<Type, List<Transition>> transitions = new();

    IState currentState;
    List<Transition> currentTransitions = new();

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == currentState)
            return;

        currentState?.OnExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        currentTransitions ??= EmptyTransitions;

        currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (transitions.TryGetValue(from.GetType(), out var matchingTransitions) == false)
        {
            matchingTransitions = new List<Transition>();
            transitions[from.GetType()] = matchingTransitions;
        }

        matchingTransitions.Add(new Transition(to, predicate));
    }

    Transition GetTransition()
    {
        return currentTransitions.FirstOrDefault(transition => transition.Condition());
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