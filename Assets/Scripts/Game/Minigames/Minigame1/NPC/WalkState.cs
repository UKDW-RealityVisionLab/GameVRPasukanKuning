using UnityEngine;

public class WalkState : State
{
    public WalkState(NPCFSMController npc, StateMachine fsm) : base(npc, fsm) { }

    public override void Enter()
    {
        npc.GoToNextWaypoint();
        npc.SetWalking(true);
    }

    public override void Update()
    {
        if (!npc.agent.pathPending && npc.agent.remainingDistance < 0.2f)
        {
            fsm.ChangeState(new IdleState(npc, fsm));
        }
    }

    public override void Exit()
    {
        npc.SetWalking(false);
    }
}
