using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingState : MoveToAndAnimateState
{
    public WonderingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
: base(sm, parent, ai, dest, "IsWondering") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
