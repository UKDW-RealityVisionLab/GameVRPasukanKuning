using UnityEngine;
public class IdleIdleState : State
{
    public IdleIdleState(StateMachine sm, State parent) : base(sm, parent) { }

    public override void Enter() => Debug.Log("Enter Idle->Idle");
    public override void Exit() => Debug.Log("Exit Idle->Idle");
    public override void Update()
    {
        // logic for staying in idle
    }
}



