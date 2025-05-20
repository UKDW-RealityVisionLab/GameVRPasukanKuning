using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadState : MoveToAndAnimateState
{
    public SadState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
: base(sm, parent, ai, dest, "IsSad") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
