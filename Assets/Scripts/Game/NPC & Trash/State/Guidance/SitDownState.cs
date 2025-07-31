using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDownState : MoveToAndAnimateState
{
    public SitDownState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsSitDown") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
