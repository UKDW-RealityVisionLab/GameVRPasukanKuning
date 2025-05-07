using UnityEngine;

public class IdleState : State
{
    private float timer = 0f;
    private float waitTime = 1f;

    public IdleState(NPCFSMController npc, StateMachine fsm) : base(npc, fsm) { }

    public override void Enter()
    {
        timer = 0f;
        npc.StopMoving();
        npc.SetWalking(false);
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            fsm.ChangeState(new WalkState(npc, fsm));
        }
    }
}
