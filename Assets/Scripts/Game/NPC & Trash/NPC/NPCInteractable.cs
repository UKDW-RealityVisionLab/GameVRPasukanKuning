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
    [SerializeField] private GameObject angryUi;
    [SerializeField] private Animator animator;
    private ChatContext chatCon;
    private AIBehaviour aiBehaviour;

    private Transform interactorTransform;
    public float rotateSpeed = 5f;
    public float maxInteractionDistance = 4f;
    public bool isAngry = false;

     void Awake()
    {
        chatCon = GetComponent<ChatContext>();
        aiBehaviour = GetComponent<AIBehaviour>();
    }

    public void Interact(Transform InteractorTransform)
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
            if (aiBehaviour.playerInteractCount >= 1)
            {
                chatCon.GetContextQuestion();
                aiBehaviour.EnterTalkingState(InteractorTransform.position);
                animator.SetBool("IsTalking", true);
            }
            if (aiBehaviour.playerInteractCount > 5)
            {
                chatCon.GetMarahContext();
                aiBehaviour.EnterTalkingAngryState(InteractorTransform.position);
                animator.SetBool("IsAngry", true);
            }
        }
        interactorTransform = InteractorTransform;

        uiNPC.SetActive(true);

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

    private void Update()
    {
        if (uiNPC.activeSelf && interactorTransform != null)
        {
            // Update rotasi
            Vector3 direction = interactorTransform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }

            // Jarak terlalu jauh = tutup
            float distance = Vector3.Distance(transform.position, interactorTransform.position);
            if (distance > maxInteractionDistance)
            {
                EndInteraction();
            }
        }
    }

    public void isGuidingPlayer()
    {

    }

    public void EndInteraction()
    {
        aiBehaviour.isInteracting = false;
        uiNPC.SetActive(false);
        animator.SetBool("IsTalking", false);
        animator.SetTrigger("IsExit");
        interactorTransform = null;
    }

    public string GetInteractText()
    {
        return interactText;
    }
}
