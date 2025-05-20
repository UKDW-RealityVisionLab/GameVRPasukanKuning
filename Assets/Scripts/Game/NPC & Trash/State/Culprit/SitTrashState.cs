using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitTrashState : MoveToAndAnimateState
{
    public SitTrashState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsSitTrash") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
