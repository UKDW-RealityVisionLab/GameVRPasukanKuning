using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MoveToAndAnimateState : State
{
    protected AIBehaviour ai;
    protected NavMeshAgent agent;
    protected Vector3 destination;
    protected string animationBoolName;
    private bool hasArrived;
    private bool animationStarted;

    public MoveToAndAnimateState(StateMachine sm, State parent, AIBehaviour aiBehaviour, Vector3 dest, string animBool)
        : base(sm, parent)
    {
        ai = aiBehaviour;
        agent = ai.agent;
        destination = dest;
        animationBoolName = animBool;
    }

    public override void Enter()
    {
        hasArrived = false;
        animationStarted = false;

        ai.ResetIdleBools();
        if (ai.isChasing == true)
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new ChasingState(ai.stateMachine, ai.wanderingState, ai, ai.targetSampah.position));
            ai.wanderingState.SetCondition("IsWandering");
        }
        else
        {
            ai.animator.SetBool("IsWalking", true);
        }

        agent.SetDestination(destination);
    }

    public override void Exit()
    {
        ai.animator.SetBool("IsWalking", false);
        ai.animator.SetBool(animationBoolName, false);
        agent.ResetPath();
    }

    public override void Update()
    {
        if (!hasArrived && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            hasArrived = true;
            ai.animator.SetBool("IsWalking", false);
            ai.animator.SetBool(animationBoolName, true);
            animationStarted = true;
        }

        if (animationStarted)
        {
            AnimatorStateInfo anim = ai.animator.GetCurrentAnimatorStateInfo(0);
            if (anim.IsName(animationBoolName) && anim.normalizedTime >= 1f)
            {
                OnAnimationComplete();
            }

        }
    }

    protected abstract void OnAnimationComplete();
}
