public class StateMachine
{
    private State currentState;

    public void ChangeState(State newState,string conditionToSetTrue = null)
    {
        currentState?.Exit();
        currentState = newState;

        if (!string.IsNullOrEmpty(conditionToSetTrue) && currentState is IAnimatorUser animatorUser)
        {
            animatorUser.SetCondition(conditionToSetTrue);
        }

        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
