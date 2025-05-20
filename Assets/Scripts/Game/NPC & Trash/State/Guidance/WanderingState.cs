using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : State, IAnimatorUser
{
    private AIBehaviour ai;

    public WanderingState(StateMachine sm, AIBehaviour aiBehaviour) : base(sm)
    {
        ai = aiBehaviour;
    }
    public void SetCondition(string conditionName)
    {
        ai.animator.SetBool(conditionName, true);
    }

    public override void Enter()
    {
        ai.ResetIdleBools();
        ai.animator.SetBool("IsWandering", true);
        ai.animator.SetBool("IsWalking", true);
    }

    public override void Exit()
    {
        ai.animator.SetBool("IsWalking", false);
        ai.animator.SetBool("IsWandering", false);
    }
}
