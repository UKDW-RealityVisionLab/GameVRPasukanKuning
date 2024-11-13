using UnityEngine;
using UnityEngine.InputSystem;

public class HandMenuInteract : MonoBehaviour
{
    public GameObject menu;  // Assign the menu GameObject here
    public InputActionReference yButtonActionReference;  // Drag the Y button action here in the Inspector

    private bool isMenuOpen = false;

    private void OnEnable()
    {
        // Enable the action and subscribe to its performed event
        yButtonActionReference.action.Enable();
        yButtonActionReference.action.performed += ToggleMenu;
    }

    private void OnDisable()
    {
        // Disable the action and unsubscribe from the event
        yButtonActionReference.action.performed -= ToggleMenu;
        yButtonActionReference.action.Disable();
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        // Toggle the menu active state
        Debug.Log("Button Y was pressed.");
        isMenuOpen = !isMenuOpen;
        menu.SetActive(isMenuOpen);
    }
}
