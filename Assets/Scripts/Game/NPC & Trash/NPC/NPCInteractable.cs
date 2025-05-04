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

    private void Awake()
    {
        npcLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform InteractorTransform)
    {
        uiNPC.SetActive(true);
        animator.SetBool("IsTalking",true);
        float playerHeight = 1.65f;
        npcLookAt.LookAtPosition(InteractorTransform.position + Vector3.up * playerHeight);
    }

    public string GetInteractText()
    {
        return interactText;
    }
}
