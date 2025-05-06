using UnityEngine;

public class DrowningState : INPCState
{
    private NPCFSM npc;
    private float drownTime = 3f;
    private float timer;

    public void Enter(NPCFSM context)
    {
        npc = context;
        npc.agent.isStopped = true;
        npc.animator.SetTrigger("Drown");

        timer = drownTime;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            npc.agent.isStopped = false;
            npc.TransitionTo(new SwimmingState());
        }
    }

    public void Exit() { }
}
