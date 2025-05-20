public abstract class State
{
    protected NPCFSMController npc;
    protected StateMachine fsm;

    public State(NPCFSMController npc, StateMachine fsm)
    {
        this.npc = npc;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}