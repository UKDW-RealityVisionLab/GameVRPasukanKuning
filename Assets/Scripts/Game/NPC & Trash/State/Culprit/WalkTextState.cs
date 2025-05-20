using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTextState : MoveToAndAnimateState
{
    public WalkTextState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsWalkText") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
