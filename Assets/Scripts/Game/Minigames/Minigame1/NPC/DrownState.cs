public class DrownState : State
{
    public DrownState(NPCFSMController npc, StateMachine fsm) : base(npc, fsm)
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
