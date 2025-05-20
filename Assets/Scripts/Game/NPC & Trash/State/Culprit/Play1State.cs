using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play1State : MoveToAndAnimateState
{
    public Play1State(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
: base(sm, parent, ai, dest, "IsPlay1") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
