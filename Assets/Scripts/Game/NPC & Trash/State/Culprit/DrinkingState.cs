using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingState : MoveToAndAnimateState
{
    public DrinkingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
    : base(sm, parent, ai, dest, "IsDrinking") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
