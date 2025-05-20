using UnityEngine;
public class TalkingState : MoveToAndAnimateState
{
    public TalkingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsTalking") { } // Sesuaikan animator parameter

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
