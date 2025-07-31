using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealState : MoveToAndAnimateState
{
    public DealState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsDeal") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
