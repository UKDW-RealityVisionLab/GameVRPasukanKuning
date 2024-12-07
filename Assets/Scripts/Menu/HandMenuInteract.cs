using UnityEngine;
using UnityEngine.InputSystem;

public class HandMenuInteract : MonoBehaviour
{
    public GameObject menu;  // Assign the menu GameObject here
    public Transform handTransform;  // Assign the Transform for the player's hand
    public Transform alternateTransform;  // Assign the Transform for the alternate location
    public InputActionReference yButtonActionReference;  // Drag the Y button action here in the Inspector

    private bool isMenuAtHand = true; // Track where the menu is

    private void OnEnable()
    {
        // Enable the action and subscribe to its performed event
        yButtonActionReference.action.Enable();
        yButtonActionReference.action.performed += ToggleMenuLocation;
    }

    private void OnDisable()
    {
        // Disable the action and unsubscribe from the event
        yButtonActionReference.action.performed -= ToggleMenuLocation;
        yButtonActionReference.action.Disable();
    }

    private void ToggleMenuLocation(InputAction.CallbackContext context)
    {
        if (menu == null)
        {
            Debug.LogWarning("Menu GameObject is not assigned.");
            return;
        }

        if (handTransform == null || alternateTransform == null)
        {
            Debug.LogWarning("Hand or alternate transform is not assigned.");
            return;
        }

        // Toggle between the hand and the alternate location
        if (isMenuAtHand)
        {
            TeleportMenu(alternateTransform);
            isMenuAtHand = false;
        }
        else
        {
            TeleportMenu(handTransform);
            isMenuAtHand = true;
        }
    }

    private void TeleportMenu(Transform targetTransform)
    {
        menu.transform.position = targetTransform.position;
        menu.transform.rotation = targetTransform.rotation;

        Debug.Log($"Menu teleported to: {targetTransform.name}");
    }
}
