using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject uiNPC;
    [SerializeField] private GameObject emotionButton;
    [SerializeField] private GameObject normalButton;
    [SerializeField] private GameObject afterNextButton;
    [SerializeField] private GameObject normalNextButton;
    [SerializeField] private GameObject choises1Button;
    [SerializeField] private GameObject choises2Button;
    [SerializeField] private GameObject[] choisesButtonList;
    [SerializeField] private Animator animator;
    private ChatContext chatCon;
    private AIBehaviour aiBehaviour;
    private PlayerInteract player;
    protected NavMeshAgent agent;

    private Transform interactorTransform;
    public float rotateSpeed = 5f;
    public float maxInteractionDistance = 4f;
    public bool isAngry = false;
    public bool isGuiding = false;
    public string destGuide;


    void Awake()
    {
        chatCon = GetComponent<ChatContext>();
        aiBehaviour = GetComponent<AIBehaviour>();
    }
    private void Update()
    {
        if (uiNPC.activeSelf || chatCon.npcDialogUI.activeSelf && interactorTransform != null)
        {
            // Update rotasi
            Vector3 direction = interactorTransform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
            //animator.SetBool("IsWalking", false);

            // Jarak terlalu jauh = tutup
            if(isGuiding == false)
            {
                maxInteractionDistance = 4f;
                float distance = Vector3.Distance(transform.position, interactorTransform.position);
                if (distance > maxInteractionDistance)
                {
                    EndInteraction();
                }
            }
            if(isGuiding == true)
            {
                if (player.middleNpc.action.WasPressedThisFrame())
                {
                    GuidingPlayerInfoHelper(player.playerBody);
                }
                maxInteractionDistance = 8f;
                float distance = Vector3.Distance(transform.position, interactorTransform.position);
                if (distance > maxInteractionDistance)
                {
                    EndInteraction();
                }
            }
        }
    }

    public void InteractGuidance(Transform InteractorTransform)
    {
        // Masuk ke TalkingState dari komponen AIBehaviour
        if (aiBehaviour != null)
        {
            aiBehaviour.isInteracting = true;
            if (aiBehaviour.playerInteractCount < 1)
            {
                chatCon.GetIntroduction();
                aiBehaviour.EnterTalkingState(InteractorTransform.position);
                animator.SetBool("IsTalking", true);
            }
            else if (aiBehaviour.playerInteractCount >= 1 && aiBehaviour.playerInteractCount <= 5)
            {
                chatCon.GetContextQuestion();
                aiBehaviour.EnterTalkingState(InteractorTransform.position);
                animator.SetBool("IsTalking", true);
            }
            else if (aiBehaviour.playerInteractCount > 5)
            {
                isAngry = true;
                chatCon.GetAngryContext();
                aiBehaviour.EnterTalkingAngryState(InteractorTransform.position);
                animator.SetBool("IsAngry", true);
                normalButton.SetActive(false);
            }
        }
        interactorTransform = InteractorTransform;
        uiNPC.SetActive(true);

        if (isAngry == true)
        {
            emotionButton.SetActive(true);
        }
    }
    public void InteractCulprit(Transform InteractorTransform)
    {
        // Masuk ke TalkingState dari komponen AIBehaviour
        if (aiBehaviour != null)
        {
            aiBehaviour.isInteracting = true;
            if (aiBehaviour.playerInteractCount >= 0 && aiBehaviour.playerInteractCount <= 5)
            {
                if (aiBehaviour.emotion.ToLower() == "happy")
                {
                    chatCon.GetEmotionChat();
                    aiBehaviour.EnterTalkingHappyCulpritState(InteractorTransform.position);
                    animator.SetBool("IsThinking", true);
                }
                else if (aiBehaviour.emotion.ToLower() == "sad")
                {
                    chatCon.GetEmotionChat();
                    aiBehaviour.EnterTalkingSadCulpritState(InteractorTransform.position);
                    animator.SetBool("IsWalkText", true);
                }
                else if (aiBehaviour.emotion.ToLower() == "wondering")
                {
                    chatCon.GetEmotionChat();
                    aiBehaviour.EnterTalkingWonderingCulpritState(InteractorTransform.position);
                    animator.SetBool("IsLaugh", true);
                }
            }
            else if (aiBehaviour.playerInteractCount > 5)
            {
                isAngry = true;
                chatCon.GetAngryContext();
                aiBehaviour.EnterTalkingAngryState(InteractorTransform.position);
                animator.SetBool("IsAngry", true);
                normalButton.SetActive(false);
            }
        }
        interactorTransform = InteractorTransform;
        uiNPC.SetActive(true);

        if (isAngry == true)
        {
            emotionButton.SetActive(true);
        }
    }
    public void InteractBystander(Transform InteractorTransform)
    {
        // Masuk ke TalkingState dari komponen AIBehaviour
        if (aiBehaviour != null)
        {
            aiBehaviour.isInteracting = true;
            if (aiBehaviour.playerInteractCount >= 0 && aiBehaviour.playerInteractCount <= 5)
            {
                if (aiBehaviour.emotion.ToLower() == "happy")
                {
                    chatCon.GetEmotionChat();
                    aiBehaviour.EnterTalkingHappyState(InteractorTransform.position);
                    animator.SetBool("IsHappy", true);
                }
                else if (aiBehaviour.emotion.ToLower() == "sad")
                {
                    chatCon.GetEmotionChat();
                    aiBehaviour.EnterTalkingSadState(InteractorTransform.position);
                    animator.SetBool("IsSad", true);
                }
                else if (aiBehaviour.emotion.ToLower() == "wondering")
                {
                    chatCon.GetEmotionChat();
                    aiBehaviour.EnterTalkingWonderingState(InteractorTransform.position);
                    animator.SetBool("IsWondering", true);
                }
            }
            else if (aiBehaviour.playerInteractCount > 5)
            {
                isAngry = true;
                chatCon.GetAngryContext();
                aiBehaviour.EnterTalkingAngryState(InteractorTransform.position);
                animator.SetBool("IsAngry", true);
                normalButton.SetActive(false);
            }
        }
        interactorTransform = InteractorTransform;
        uiNPC.SetActive(true);

        if (isAngry == true)
        {
            emotionButton.SetActive(true);
        }
    }

    public void GuidingPlayerInfoHelper(Transform place)
    {
        if (aiBehaviour == null || place == null) return;

        aiBehaviour.animator.SetTrigger("IsExit");
        aiBehaviour.stateMachine.ChangeState(aiBehaviour.guidanceState);
        aiBehaviour.guidanceState.ChangeSubState(new WalkWithPlayerState(aiBehaviour.stateMachine, aiBehaviour.guidanceState, aiBehaviour, player.playerBody.position));
        aiBehaviour.guidanceState.SetCondition("IsGuiding");
        isGuiding = true;
    }

    public void WalkingToDestination(Transform place)
    {
        if (aiBehaviour == null || place == null) return;
        isGuiding = true;
        aiBehaviour.animator.SetTrigger("IsExit");
        aiBehaviour.stateMachine.ChangeState(aiBehaviour.guidanceState);
        aiBehaviour.guidanceState.ChangeSubState(new WalkingGuideState(aiBehaviour.stateMachine, aiBehaviour.guidanceState, aiBehaviour, place.position));
        aiBehaviour.guidanceState.SetCondition("IsGuiding");
        if (Vector3.Distance(transform.position, place.position) < 0.5f)
        {
            GuidingPlayerInfoHelper(place);
        }
    }

    public void GuideButtonContext(string destination)
    {
        // Kirim request ke ChatContext agar bisa menampilkan teks sesuai tujuan
        if (chatCon != null)
        {
            chatCon.SetCurrentGuideDestination(destination);
            chatCon.GetGuideContext();
        }
    }
    public void isGuidingPlayerSeller(Transform place, string destination)
    {
        if (aiBehaviour == null) return;

        aiBehaviour.animator.SetTrigger("IsExit");
        aiBehaviour.stateMachine.ChangeState(aiBehaviour.guidanceState);
        aiBehaviour.guidanceState.ChangeSubState(new WalkWithPlayerState(aiBehaviour.stateMachine, aiBehaviour.guidanceState, aiBehaviour, place.position));
        aiBehaviour.guidanceState.SetCondition("IsGuiding");

        // Kirim request ke ChatContext agar bisa menampilkan teks sesuai tujuan
        if (chatCon != null)
        {
            chatCon.SetCurrentGuideDestination(destination);
            chatCon.GetGuideContext();
        }
    }
    public void isGuidingPlayerCrafter(Transform place, string destination)
    {
        if (aiBehaviour == null) return;

        aiBehaviour.animator.SetTrigger("IsExit");
        aiBehaviour.stateMachine.ChangeState(aiBehaviour.guidanceState);
        aiBehaviour.guidanceState.ChangeSubState(new WalkWithPlayerState(aiBehaviour.stateMachine, aiBehaviour.guidanceState, aiBehaviour, place.position));
        aiBehaviour.guidanceState.SetCondition("IsGuiding");

        // Kirim request ke ChatContext agar bisa menampilkan teks sesuai tujuan
        if (chatCon != null)
        {
            chatCon.SetCurrentGuideDestination(destination);
            chatCon.GetGuideContext();
        }
    }
    public void Apologize()
    {
        aiBehaviour.playerInteractCount = 1;
        isAngry = false;
        chatCon.GetAfterAngryChat();
        emotionButton.SetActive(false);
        normalButton.SetActive(true);
    }
    public void EndInteraction()
    {
        isGuiding = false;
        aiBehaviour.isInteracting = false;
        uiNPC.SetActive(false);
        normalNextButton.SetActive(true);
        afterNextButton.SetActive(false);
        choises1Button.SetActive(false);
        choises2Button.SetActive(false);
        animator.SetBool("IsTalking", false);
        animator.SetBool("IsHappy", false);
        animator.SetBool("IsSad", false);
        animator.SetBool("IsWondering", false);
        animator.SetTrigger("IsExit");
        interactorTransform = null;
        for (int i = 0; i < choisesButtonList.Length; i++)
        {
            choisesButtonList[i].SetActive(false);
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

}
