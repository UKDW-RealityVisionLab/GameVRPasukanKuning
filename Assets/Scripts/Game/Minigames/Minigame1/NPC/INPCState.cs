public interface INPCState
{
    void Enter(NPCFSM context);
    void Update();
    void Exit();
}
