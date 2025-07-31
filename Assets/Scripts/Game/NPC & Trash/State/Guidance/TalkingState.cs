using UnityEngine;
public class TalkingState : MoveToAndAnimateState
{
    private float talkingDuration = 5f;
    private float timer = 0f;
    public TalkingState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsTalking") { } // Sesuaikan animator parameter


    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
