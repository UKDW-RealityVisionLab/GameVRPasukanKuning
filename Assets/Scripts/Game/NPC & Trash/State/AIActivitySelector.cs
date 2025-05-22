using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class AIActivitySelector
{
    private AIBehaviour ai;
    private ChatContext chatCon;
    private ListContext listCon;

    public AIActivitySelector(AIBehaviour ai)
    {
        this.ai = ai;
        chatCon = ai.GetComponent<ChatContext>();
    }

    public void ChooseRandomActivity()
    {
        if (ai.isInteracting == true) return;

        ai.availableActions.Clear();

        switch (ai.Type)
        {
            case NPCType.GuidanceSeller:
                AddSellerActions();
                if (ai.wantToArrange > 4)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    chatCon.GetRandomChat();
                    ai.isTired++;
                    ai.wantToArrange = 0;
                    break;
                }
                if (ai.isTired > 6)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    chatCon.GetRandomChat();
                    ai.isTired = 0;
                    break;
                }
                break;
            case NPCType.GuidanceCrafter:
                AddCrafterActions();
                if (ai.wantToArrange > 2)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    chatCon.GetRandomChat();
                    ai.isTired++;
                    ai.wantToArrange = 0;
                    break;
                }
                if (ai.isTired > 8)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    chatCon.GetRandomChat();
                    ai.isTired = 0;
                    break;
                }
                break;
            case NPCType.GuidanceInfoHelper:
                AddInfoHelperActions();
                if (ai.wantToArrange > 5)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    chatCon.GetRandomChat();
                    ai.isTired++;
                    ai.wantToArrange = 0;
                    break;
                }
                if (ai.isTired > 4)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    chatCon.GetRandomChat();
                    ai.isTired = 0;
                    break;
                }
                break;
            case NPCType.CulpritMale:
                AddMaleActions();
                if (ai.activityDone > 5)
                {
                    ai.hasThirsty++;
                    if (ai.hasThirsty > 3)
                    {
                        ai.animator.SetTrigger("IsExit");
                        Vector3 randomPos = ai.GetRandomNavmeshPosition();
                        ai.stateMachine.ChangeState(ai.activityState);
                        ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
                        ai.activityState.SetCondition("IsActiving");
                        ai.hasThirsty = 0;
                        break;
                    }
                    break;
                }
                if (ai.isTired >= 1)
                {
                    ai.animator.SetTrigger("IsExit");
                    Vector3 randomPos = ai.GetRandomNavmeshPosition();
                    ai.stateMachine.ChangeState(ai.activityState);
                    ai.activityState.ChangeSubState(new SmokingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
                    ai.activityState.SetCondition("IsActiving");
                    ai.DropItem();
                    ai.isTired = 0;
                    break;
                }
                break;
            case NPCType.CulpritOldman:
                AddOldmanActions();
                if(ai.activityDone > 3)
                {
                    ai.hasThirsty++;
                    if (ai.hasThirsty > 2)
                    {
                        ai.animator.SetTrigger("IsExit");
                        ai.stateMachine.ChangeState(ai.activityState);
                        ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
                        ai.activityState.SetCondition("IsActiving");
                        ai.hasThirsty = 0;
                        ai.activityDone = 0;
                        break;
                    }
                    break;
                }
                if (ai.isTired >= 2)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.activityState);
                    ai.activityState.ChangeSubState(new SmokingState(ai.stateMachine, ai.activityState, ai, ai.randomPos));
                    ai.activityState.SetCondition("IsActiving");
                    ai.DropItem();
                    ai.isTired = 0;
                    break;
                }
                break;
            case NPCType.CulpritChild:
                AddCulpritChildActions();
                if (ai.activityDone > 3)
                {
                    ai.hasThirsty++;
                    if (ai.hasThirsty > 2)
                    {
                        ai.animator.SetTrigger("IsExit");
                        ai.stateMachine.ChangeState(ai.activityState);
                        ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, ai.transform.position));
                        ai.activityState.SetCondition("IsActiving");
                        ai.hasThirsty = 0;
                        ai.activityDone = 0;
                        break;
                    }
                    break;
                }
                if (ai.isTired >= 3)
                {
                    ai.animator.SetTrigger("IsExit");
                    //Vector3 randomPos = ai.GetRandomNavmeshPosition();
                    ai.stateMachine.ChangeState(ai.idleState);
                    ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, ai.transform.position));
                    ai.idleState.SetCondition("IsIdleNother");
                    ai.isTired = 0;
                    break;
                }
                break;
            case NPCType.BystanderWoman:
                AddWomanActions();
                break;
            case NPCType.BystanderTourist:
                AddTouristActions();
                if (ai.wantToTakePhoto > 3)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.wanderingState);
                    ai.wanderingState.ChangeSubState(new TakePhotoState(ai.stateMachine, ai.wanderingState, ai, ai.GetNearestSpotFoto().position));
                    ai.wanderingState.SetCondition("IsWandering");
                    ai.wantToTakePhoto = 0;
                    break;
                }
                if (ai.hasThirsty > 2)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.wanderingState);
                    ai.wanderingState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.wanderingState, ai, ai.GetNearestChairPosition().position));
                    ai.wanderingState.SetCondition("IsWandering");
                    ai.hasThirsty = 0;
                    break;
                }
                break;
            case NPCType.BystanderChild:
                AddBystanderChildActions();
                if (ai.isBored > 4)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.activityState);
                    ai.activityState.ChangeSubState(new Play2State(ai.stateMachine, ai.activityState, ai, ai.transform.position));
                    ai.activityState.SetCondition("IsActiving");
                    ai.hasThirsty++;
                    ai.isBored = 0;
                    break;
                }
                if (ai.hasThirsty > 6)
                {
                    ai.animator.SetTrigger("IsExit");
                    ai.stateMachine.ChangeState(ai.wanderingState);
                    ai.wanderingState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.wanderingState, ai, ai.transform.position));
                    ai.wanderingState.SetCondition("IsWandering");
                    ai.hasThirsty = 0;
                    break;
                }
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
            chatCon.GetRandomChat();
            ai.isTired++;
            ai.wantToArrange--;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
            chatCon.GetRandomChat();
            ai.wantToArrange++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SellState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
            chatCon.GetRandomChat();
            ai.isTired++;
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
            chatCon.GetRandomChat();
            ai.isTired++;
            ai.wantToArrange--;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SellState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
            chatCon.GetRandomChat();
            ai.isTired++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetSampah.position));
            ai.idleState.SetCondition("IsIdleNother");
            chatCon.GetRandomChat();
            ai.wantToArrange++;
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
            chatCon.GetRandomChat();
            ai.isTired++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ArrangeStuffState(ai.stateMachine, ai.idleState, ai, ai.targetNataBarang.position));
            ai.idleState.SetCondition("IsIdleNother");
            chatCon.GetRandomChat();
            ai.isTired++;
            ai.wantToArrange--;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SitDownState(ai.stateMachine, ai.idleState, ai, ai.targetCustomer.position));
            ai.idleState.SetCondition("IsIdleNother");
            chatCon.GetRandomChat();
            ai.wantToArrange++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.guidanceState);
            ai.guidanceState.ChangeSubState(new WalkWithPlayerState(ai.stateMachine, ai.guidanceState, ai, ai.targetCustomer.position));
            ai.guidanceState.SetCondition("IsGuiding");
            chatCon.GetRandomChat();
            ai.isTired++;
        });
    }

    private void AddMaleActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new WalkTextState(ai.stateMachine, ai.wanderingState, ai, randomPos));
            ai.wanderingState.SetCondition("IsWandering");
            ai.activityDone++;
            ai.isTired++;
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
            ai.activityState.ChangeSubState(new TrashState(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
            ai.activityDone++;
            ai.isTired++;
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
            ai.activityState.ChangeSubState(new SmokingState(ai.stateMachine, ai.activityState, ai, ai.transform.position));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
            ai.hasThirsty++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.activityDone++;
        });
    }

    private void AddOldmanActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            //Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, ai.transform.position));
            ai.idleState.SetCondition("IsIdleNother");
            ai.activityDone++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new SitDownState(ai.stateMachine, ai.wanderingState, ai, ai.GetRandomChairPosition().position));
            ai.wanderingState.SetCondition("IsWandering");
            ai.activityDone++;
            ai.isTired++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new TrashState(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
            ai.activityDone++;
            ai.isTired++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitDownState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
            ai.activityDone++;
            ai.isTired--;
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
            ai.activityState.ChangeSubState(new SmokingState(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
            ai.hasThirsty++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
        });
    }
    private void AddCulpritChildActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new ThinkingState(ai.stateMachine, ai.idleState, ai, ai.transform.position));
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
            ai.activityState.ChangeSubState(new TrashState(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.DropItem();
            ai.activityDone++;
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
            ai.activityDone++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new WalkTextState(ai.stateMachine, ai.wanderingState, ai, randomPos));
            ai.wanderingState.SetCondition("IsWandering");
            ai.activityDone++;
            ai.isTired++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new Play1State(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.activityDone++;
            ai.isTired++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new DrinkingState(ai.stateMachine, ai.activityState, ai, randomPos));
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
    }
    private void AddTouristActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new WonderingState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.wantToTakePhoto++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new HappyState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.wantToTakePhoto++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SadState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.hasThirsty++;
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
            ai.stateMachine.ChangeState(ai.wanderingState);
            ai.wanderingState.ChangeSubState(new TakePhotoState(ai.stateMachine, ai.wanderingState, ai, ai.GetSpotFoto().position));
            ai.wanderingState.SetCondition("IsWandering");
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new SitDownState(ai.stateMachine, ai.activityState, ai, ai.GetRandomChairPosition().position));
            ai.activityState.SetCondition("IsActiving");
            ai.wantToTakePhoto++;
        });
    }
    private void AddBystanderChildActions()
    {
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new WonderingState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.isBored++;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new HappyState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.isBored--;
        });

        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.idleState);
            ai.idleState.ChangeSubState(new SadState(ai.stateMachine, ai.idleState, ai, randomPos));
            ai.idleState.SetCondition("IsIdleNother");
            ai.isBored++;
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
            ai.hasThirsty--;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new Play2State(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.hasThirsty++;
        });
        ai.availableActions.Add(() =>
        {
            ai.animator.SetTrigger("IsExit");
            Vector3 randomPos = ai.GetRandomNavmeshPosition();
            ai.stateMachine.ChangeState(ai.activityState);
            ai.activityState.ChangeSubState(new Play1State(ai.stateMachine, ai.activityState, ai, randomPos));
            ai.activityState.SetCondition("IsActiving");
            ai.hasThirsty++;
        });
    }
}
