public class StateMachine2
{
    public State2 currentState;

    public void Initialize(State2 startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(State2 newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
