using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingGuideState : MoveToAndAnimateState
{
    public WalkingGuideState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsWalking") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
