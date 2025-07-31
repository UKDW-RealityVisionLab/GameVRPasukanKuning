using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceState : State, IAnimatorUser
{
    private AIBehaviour ai;
    public GuidanceState(StateMachine sm, AIBehaviour aiBehaviour) : base(sm)
    {
        this.ai = aiBehaviour;
    }

    public void SetCondition(string conditionName)
    {
        ai.animator.SetBool(conditionName, true);
    }

    public override void Enter()
    {
        ai.ResetIdleBools();
        ai.animator.SetBool("IsGuiding", true);
    }

    public override void Exit()
    {
        ai.animator.SetBool("IsGuiding", false);
    }
}
