using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInPlaceState : MoveToAndAnimateState
{
    public TrashInPlaceState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
    : base(sm, parent, ai, dest, "IsThrowTrashinPlace") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
