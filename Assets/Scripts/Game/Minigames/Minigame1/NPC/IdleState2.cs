using UnityEngine;

public class IdleState2 : State2
{
    private float timer = 0f;
    private float waitTime = 1f;

    public IdleState2(NPCFSMController npc, StateMachine2 fsm) : base(npc, fsm) { }

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
