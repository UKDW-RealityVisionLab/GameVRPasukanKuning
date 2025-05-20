using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellState : MoveToAndAnimateState
{
    public SellState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsTalking") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
