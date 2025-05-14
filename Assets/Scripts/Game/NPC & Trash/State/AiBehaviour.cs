using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    public string roleAi;
    public NPCType Type;
    public Animator animator;
    public NavMeshAgent agent;
    private float timer;
    private float distance = 2f;
    public float actionInterval = 5f; // waktu antar aktivitas acak
    private List<System.Action> availableActions = new List<System.Action>();
    public bool isInteracting = false;


    public Transform targetNataBarang;
    public Transform targetSampah;
    public Transform targetCustomer;

    private StateMachine stateMachine;
    private IdleState idleState;
    private WanderingState wanderingState;
    private ActivityState activityState;
    private GuidanceState guidanceState;

    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new IdleState(stateMachine, this);
        wanderingState = new WanderingState(stateMachine, this);
        activityState = new ActivityState(stateMachine, this);
        guidanceState = new GuidanceState(stateMachine, this);
        //StartCoroutine(RunRoutineByRole());
        stateMachine.ChangeState(idleState);
    }

    void Update()
    {
        if (isInteracting == false) // Hanya lakukan aktivitas kalau tidak interaksi
        {
            timer += Time.deltaTime;
            if (timer >= actionInterval)
            {
                timer = 0f;
                ChooseRandomActivity();
            }
        }
        else
        {
            return;
        }

            stateMachine.Update();
    }

    void ChooseRandomActivity()
    {
        if (isInteracting == true) return;
        availableActions.Clear();

        if (Type == NPCType.GuidanceSeller)
        {
            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(idleState);
                idleState.ChangeSubState(new ArrangeStuffState(stateMachine, idleState, this, targetNataBarang.position));
                idleState.SetCondition("IsIdleNother");
            });

            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(wanderingState);
                wanderingState.ChangeSubState(new TrashState(stateMachine, wanderingState, this, targetSampah.position));
                wanderingState.SetCondition("IsWandering");
            });

            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(idleState);
                idleState.ChangeSubState(new SellState(stateMachine, idleState, this, targetCustomer.position));
                idleState.SetCondition("IsIdleNother");
            });
        }
        if (Type == NPCType.GuidanceCrafter)
        {
            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(idleState);
                idleState.ChangeSubState(new ArrangeStuffState(stateMachine, idleState, this, targetNataBarang.position));
                idleState.SetCondition("IsIdleNother");
            });

            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(wanderingState);
                wanderingState.ChangeSubState(new TrashState(stateMachine, wanderingState, this, targetSampah.position));
                wanderingState.SetCondition("IsWandering");
            });
        }
        if (Type == NPCType.GuidanceInfoHelper)
        {
            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(guidanceState);
                guidanceState.ChangeSubState(new QuestionState(stateMachine, guidanceState, this, targetCustomer.position));
                guidanceState.SetCondition("IsGuiding");
            });
            availableActions.Add(() =>
            {
                animator.SetTrigger("IsExit");
                stateMachine.ChangeState(idleState);
                idleState.ChangeSubState(new ArrangeStuffState(stateMachine, idleState, this, targetNataBarang.position));
                idleState.SetCondition("IsIdleNother");
            });
        }

        if (availableActions.Count > 0 && isInteracting == false)
        {
            int index = Random.Range(0, availableActions.Count);
            availableActions[index].Invoke();
        }
    }

    public void EnterTalkingState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new TalkingState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }

    public void ResetIdleBools()
    {
        animator.SetBool("IsWandering", false);
        animator.SetBool("IsIdleNother", false);
        animator.SetBool("IsTalking", false);
        animator.SetBool("IsArrangeStuff", false);
        animator.SetBool("IsThrowTrash", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsOffering", false);
        animator.SetBool("IsAnswer", false);
        animator.SetBool("IsQuestion", false);
    }
}

