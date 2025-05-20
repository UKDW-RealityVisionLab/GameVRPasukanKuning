public class SwimState : State
{
    public SwimState(NPCFSMController npc, StateMachine fsm) : base(npc, fsm)
    {
        this.npc = npc;
    }

    public override void Enter()
    {
        npc.SetSwimming(true);
        npc.SetWalking(false);
        npc.GoToNextWaypoint();
    }

    public override void Update()
    {
        if (!npc.agent.pathPending && npc.agent.remainingDistance <= npc.agent.stoppingDistance)
        {
            npc.GoToNextWaypoint();
        }
    }

    public override void Exit()
    {
        npc.SetSwimming(false);
    }
}
