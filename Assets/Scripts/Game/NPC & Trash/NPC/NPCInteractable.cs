using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject uiNPC;
    [SerializeField] private GameObject emotionButton;
    [SerializeField] private GameObject normalButton;
    [SerializeField] private Animator animator;
    private ChatContext chatCon;
    private AIBehaviour aiBehaviour;

    private Transform interactorTransform;
    public float rotateSpeed = 5f;
    public float maxInteractionDistance = 4f;
    public bool isAngry = false;
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
            animator.SetBool("IsWalking", false);

            // Jarak terlalu jauh = tutup
            float distance = Vector3.Distance(transform.position, interactorTransform.position);
            if (distance > maxInteractionDistance)
            {
                EndInteraction();
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
    //public void InteractAngry(Transform InteractorTransform)
    //{
    //    // Masuk ke TalkingState dari komponen AIBehaviour
    //    if (aiBehaviour != null)
    //    {
    //        aiBehaviour.isInteracting = true;

    //    }
    //    interactorTransform = InteractorTransform;

    //    angryUi.SetActive(true);
    //    animator.SetBool("IsAngry", true);
    //}

    public void GuidingPlayerInfoHelper(Transform place)
    {
        if (aiBehaviour == null || place == null) return;
   
        aiBehaviour.animator.SetTrigger("IsExit");
        aiBehaviour.stateMachine.ChangeState(aiBehaviour.guidanceState);
        aiBehaviour.guidanceState.ChangeSubState(new WalkWithPlayerState(aiBehaviour.stateMachine, aiBehaviour.guidanceState, aiBehaviour, place.position));
        aiBehaviour.guidanceState.SetCondition("IsGuiding");
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
        aiBehaviour.isInteracting = false;
        uiNPC.SetActive(false);
        animator.SetBool("IsTalking", false);
        animator.SetBool("IsHappy", false);
        animator.SetBool("IsSad", false);
        animator.SetBool("IsWondering", false);
        animator.SetTrigger("IsExit");
        interactorTransform = null;
    }

    public string GetInteractText()
    {
        return interactText;
    }
}
