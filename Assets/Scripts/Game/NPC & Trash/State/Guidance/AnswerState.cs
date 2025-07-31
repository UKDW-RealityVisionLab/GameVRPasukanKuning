using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerState : MoveToAndAnimateState
{
    public AnswerState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsAnswering") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
