using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWithPlayerState : MoveToAndAnimateState
{
    public WalkWithPlayerState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsWithPlayer") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
