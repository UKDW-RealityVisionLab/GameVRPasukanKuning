using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2State : MoveToAndAnimateState
{
    public Play2State(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
: base(sm, parent, ai, dest, "IsPlay2") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
