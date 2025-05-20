using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferState : MoveToAndAnimateState
{
    public OfferState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsOffering") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
