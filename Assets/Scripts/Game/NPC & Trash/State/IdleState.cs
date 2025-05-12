using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State, IAnimatorUser
{
    private AIBehaviour ai;

    public IdleState(StateMachine sm, AIBehaviour aiBehaviour) : base(sm)
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
        ai.animator.SetBool("IsIdleNother", true);
    }

    public override void Exit()
    {
        ai.animator.SetBool("IsIdleNother", false);
    }
}

