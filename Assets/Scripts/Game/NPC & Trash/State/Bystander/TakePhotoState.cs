using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotoState : MoveToAndAnimateState
{
    public TakePhotoState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsTakePhoto") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
