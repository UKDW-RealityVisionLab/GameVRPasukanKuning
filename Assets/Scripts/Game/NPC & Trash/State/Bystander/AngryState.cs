using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryState : MoveToAndAnimateState
{
    public AngryState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
: base(sm, parent, ai, dest, "IsAngry") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
