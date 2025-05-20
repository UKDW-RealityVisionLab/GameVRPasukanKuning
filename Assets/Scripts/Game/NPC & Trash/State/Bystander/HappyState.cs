using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyState : MoveToAndAnimateState
{
    public HappyState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
    : base(sm, parent, ai, dest, "IsHappy") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
