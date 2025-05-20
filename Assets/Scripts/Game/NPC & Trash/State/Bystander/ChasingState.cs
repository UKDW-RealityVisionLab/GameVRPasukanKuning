using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : MoveToAndAnimateState
{
    public ChasingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
    : base(sm, parent, ai, dest, "IsChasing") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
