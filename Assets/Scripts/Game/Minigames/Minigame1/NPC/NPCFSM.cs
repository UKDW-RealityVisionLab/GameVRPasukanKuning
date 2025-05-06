using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPCFSM : MonoBehaviour
{
    private INPCState currentState;

    [Header("Dependencies")]
    public NavMeshAgent agent;
    public Animator animator;
    public RipCurrentController ripController;

    [Header("Flags")]
    public List<Transform> flags;
    public int currentFlagIndex = 0;

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();

        TransitionTo(new WalkingToFlagState());
    }

    void Update()
    {
        currentState?.Update();
    }

    public void TransitionTo(INPCState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void TriggerRipCurrent(int sectorIndex)
    {
        ripController.TriggerRipCurrentsAtStart();
    }
}
