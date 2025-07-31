using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionState : MoveToAndAnimateState
{
    public QuestionState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsQuestioning") { }
    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
