using UnityEngine;

public class SwimmingState : INPCState
{
    private NPCFSM npc;

    public void Enter(NPCFSM context)
    {
        npc = context;
        npc.animator.SetBool("IsSwimming", true);

        npc.currentFlagIndex++;

        if (npc.currentFlagIndex < npc.flags.Count)
        {
            npc.TransitionTo(new WalkingToFlagState());
        }
        else
        {
            Debug.Log("NPC finished visiting all flags.");
        }
    }

    public void Update() { }

    public void Exit()
    {
        npc.animator.SetBool("IsSwimming", false);
    }
}
