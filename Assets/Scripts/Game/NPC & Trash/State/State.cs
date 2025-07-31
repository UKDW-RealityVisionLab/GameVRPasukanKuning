using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    protected State parentState;

    public State(StateMachine sm, State parent = null)
    {
        stateMachine = sm;
        parentState = parent;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void ChangeSubState(State subState)
    {
        stateMachine.ChangeState(subState);
    }
}
