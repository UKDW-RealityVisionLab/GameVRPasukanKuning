using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public InputActionProperty rightNpc;
    public Transform playerBody;
    private float interactRange = 2f;
    // Update is called once per frame
    void Update()
    {
        if (rightNpc.action.ReadValue<float>() >= 0.1f)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable) 
                    && collider.TryGetComponent(out ChatContext chatContext))
                {
                    chatContext.GetContext();
                    npcInteractable.Interact(playerBody);
                }
            }
        }
    }

    public NPCInteractable GetInteractableObject()
    {
        List<NPCInteractable> npcInteractableList = new List<NPCInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable) 
                && collider.TryGetComponent(out ChatContext chatContext))
            {
                npcInteractableList.Add(npcInteractable);
            }
        }

        NPCInteractable closestNPCInteractable = null;
        foreach(NPCInteractable npcInteractable in npcInteractableList)
        {
            if(closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.transform.position) < 
                    Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    // closest
                    closestNPCInteractable = npcInteractable;
                }
            }
        }

        return closestNPCInteractable;
    }
}
