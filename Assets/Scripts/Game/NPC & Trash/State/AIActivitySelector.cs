using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class AIActivitySelector
{
    private AIBehaviour ai;

    public AIActivitySelector(AIBehaviour ai)
    {
        this.ai = ai;
    }

    public void ChooseRandomActivity()
    {
        if (ai.isInteracting == true) return;

        ai.availableActions.Clear();

        switch (ai.Type)
        {
            case NPCType.GuidanceSeller:
                AddSellerActions();
                break;
            case NPCType.GuidanceCrafter:
                AddCrafterActions();
                break;
            case NPCType.GuidanceInfoHelper:
                AddInfoHelperActions();
                break;
            case NPCType.CulpritMale:
                AddMaleActions();
                break;
            case NPCType.CulpritOldman:
                AddOldmanActions();
                break;
            case NPCType.CulpritChild:
                AddCulpritChildActions();
                break;
            case NPCType.BystanderWoman:
                AddWomanActions();
                break;
            case NPCType.BystanderSecurity:
                AddSecurityActions();
                break;
            case NPCType.BystanderChild:
                AddBystanderChildActions();
                break;
        }

        if (ai.availableActions.Count > 0 && ai.isInteracting == false)
        {
            int index = UnityEngine.Random.Range(0, ai.availableActions.Count);
            ai.availableActions[index].Invoke();
        }
    }

    private void AddSellerActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SellState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
        });
    }

    private void AddCrafterActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SellState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetSampah.position));
            ai.idleState.SetCondition("IsIdleNother");
        });
    }

    private void AddInfoHelperActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.guidanceState);
            ai.guidanceState.ChangeSubState(new QuestionState(ai.stateMachine, ai.guidanceState, ai, ai.targetCustomer.position));
            ai.guidanceState.SetCondition("IsGuiding");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.guidanceState);
            ai.guidanceState.ChangeSubState(new WalkWithPlayerState(ai.stateMachine, ai.guidanceState, ai, ai.targetCustomer.position));
            ai.guidanceState.SetCondition("IsGuiding");
        });
    }

    private void AddMaleActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            //Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            //Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new WalkTextState(ai.stateMachine, ai.wanderingState, ai, ai.randomPos));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new SitDownState(ai.stateMachine, ai.wanderingState, ai, ai.GetRandomChairPosition().position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new TrashState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitDownState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitTrashState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SmokingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
        });
    }

    private void AddOldmanActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            //Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new SitDownState(ai.stateMachine, ai.wanderingState, ai, ai.GetRandomChairPosition().position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new TrashState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitDownState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitTrashState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SmokingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
        });
    }
    private void AddCulpritChildActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            //Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new SitDownState(ai.stateMachine, ai.wanderingState, ai, ai.GetNearestChairPosition().position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new TrashState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitDownState(ai.stateMachine, ai.activityState, ai, ai.GetNearestChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitTrashState(ai.stateMachine, ai.activityState, ai, ai.GetNearestChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new WalkTextState(ai.stateMachine, ai.wanderingState, ai, ai.randomPos));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new Play1State(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
        });
    }
    private void AddWomanActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new WonderingState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new HappyState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SadState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new TrashInPlaceState(ai.stateMachine, ai.wanderingState, ai, ai.targetSampah.position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.wanderingState, ai, ai.randomPos));
            ai.wanderingState.SetCondition("IsWandering");
        });
    }
    private void AddSecurityActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new WonderingState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new HappyState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SadState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new TrashInPlaceState(ai.stateMachine, ai.wanderingState, ai, ai.targetSampah.position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.wanderingState, ai, randomPos));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitDownState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
        });
    }
    private void AddBystanderChildActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new WonderingState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new HappyState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SadState(ai.stateMachine, ai.idleState, ai, ai.randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new TrashInPlaceState(ai.stateMachine, ai.wanderingState, ai, ai.targetSampah.position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.wanderingState, ai, ai.randomPos));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new Play2State(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new Play1State(ai.stateMachine, ai.activityState, ai, ai.randomPos));
            ai.activityState.SetCondition("IsActiving");
        });
    }
}
