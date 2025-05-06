using UnityEngine;

public class WalkingToFlagState : INPCState
{
    private NPCFSM npc;

    public void Enter(NPCFSM context)
    {
        npc = context;

        if (npc.currentFlagIndex < npc.flags.Count)
        {
            npc.agent.SetDestination(npc.flags[npc.currentFlagIndex].position);
            npc.animator.SetBool("IsSwimming", false);
        }
    }

    public void Update()
    {
        if (!npc.agent.pathPending && npc.agent.remainingDistance < 1.5f)
        {
            int sectorIndex = DetermineSector(npc.flags[npc.currentFlagIndex].position);
            if (npc.ripController.IsRipCurrent(sectorIndex))
            {
                npc.TransitionTo(new DrowningState());
            }
            else
            {
                npc.TransitionTo(new SwimmingState());
            }
        }
    }

    public void Exit() { }

    private int DetermineSector(Vector3 pos)
    {
        if (pos.x < -2f) return 0;
        else if (pos.x > 2f) return 2;
        else return 1;
    }
}
