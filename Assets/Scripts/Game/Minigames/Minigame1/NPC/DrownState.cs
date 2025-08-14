public class DrownState : State2
{
    public DrownState(NPCFSMController npc, StateMachine2 fsm) : base(npc, fsm)
    {
        this.npc = npc;
    }

    public override void Enter()
    {
        npc.SetWalking(false);
        npc.SetSwimming(false);
        npc.TriggerDrowning();
        npc.isDrowning = true;
        npc.InvokeOnDrowned();
    }

    public override void Exit()
    {
        npc.isDrowning = false;
    }
}
