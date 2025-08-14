using UnityEngine;

public class WalkState : State2
{
    public WalkState(NPCFSMController npc, StateMachine2 fsm) : base(npc, fsm) { }

    public override void Enter()
    {
        npc.GoToNextWaypoint();
        npc.SetWalking(true);
    }

    public override void Update()
    {
        if (!npc.agent.pathPending && npc.agent.remainingDistance < 0.2f)
        {
            fsm.ChangeState(new IdleState2(npc, fsm));
        }
    }

    public override void Exit()
    {
        npc.SetWalking(false);
    }
}
