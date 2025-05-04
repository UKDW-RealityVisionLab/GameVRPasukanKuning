using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMesh;

    // Update is called once per frame
    void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(NPCInteractable npcInter)
    {
        containerGameObject.SetActive(true);
        interactTextMesh.text = npcInter.GetInteractText();

    }
    private void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
