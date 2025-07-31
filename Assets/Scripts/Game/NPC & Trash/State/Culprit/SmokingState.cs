using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingState : MoveToAndAnimateState
{
    public SmokingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
: base(sm, parent, ai, dest, "IsSmoking") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
