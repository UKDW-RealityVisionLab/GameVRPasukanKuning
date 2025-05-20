using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashState : MoveToAndAnimateState
{
    public TrashState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsThrowTrash") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
