public abstract class State2
{
    protected NPCFSMController npc;
    protected StateMachine2 fsm;

    public State2(NPCFSMController npc, StateMachine2 fsm)
    {
        this.npc = npc;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}