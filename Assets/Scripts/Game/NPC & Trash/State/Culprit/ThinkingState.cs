using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingState : MoveToAndAnimateState
{
    public ThinkingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsThinking") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
