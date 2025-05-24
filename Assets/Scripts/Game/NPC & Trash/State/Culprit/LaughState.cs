using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughState : MoveToAndAnimateState
{
    public LaughState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsLaugh") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
