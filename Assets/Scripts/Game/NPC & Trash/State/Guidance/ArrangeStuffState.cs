using UnityEngine;
public class ArrangeStuffState : MoveToAndAnimateState
{
    public ArrangeStuffState(StateMachine sm, State parent, AIBehaviour ai, Vector3 dest)
        : base(sm, parent, ai, dest, "IsArrangeStuff") { }

    protected override void OnAnimationComplete()
    {
        ai.animator.SetTrigger("IsExit");
    }
}
