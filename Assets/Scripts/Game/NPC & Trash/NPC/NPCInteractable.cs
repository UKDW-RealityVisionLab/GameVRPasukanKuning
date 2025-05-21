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
    [SerializeField] private Animator animator;
    private NPCHeadLookAt npcLookAt;
    private AIBehaviour aiBehaviour;

    private Transform interactorTransform;
    public float rotateSpeed = 5f;
    public float maxInteractionDistance = 4f;

    private void Awake()
    {
        npcLookAt = GetComponent<NPCHeadLookAt>();
        aiBehaviour = GetComponent<AIBehaviour>();
    }

    public void Interact(Transform InteractorTransform)
    {
        // Masuk ke TalkingState dari komponen AIBehaviour
        if (aiBehaviour != null)
        {
            aiBehaviour.isInteracting = true;
            aiBehaviour.EnterTalkingState(InteractorTransform.position);
        }
        interactorTransform = InteractorTransform;

        uiNPC.SetActive(true);
        animator.SetBool("IsTalking", true);

        float playerHeight = 1.65f;
        npcLookAt.LookAtPosition(InteractorTransform.position + Vector3.up * playerHeight);
    }

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
