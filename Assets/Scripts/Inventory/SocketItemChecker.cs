using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class SocketItemChecker : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket; // Reference to the XR Socket Interactor
    public TextMeshProUGUI itemText; // UI element for displaying the item count
    public Slot slot; // Reference to your Slot script

    private void OnEnable()
    {
        if (socket != null)
        {
            // Subscribe to the socket's events
            socket.selectEntered.AddListener(OnItemInserted);
            socket.selectExited.AddListener(OnItemRemoved);
        }
    }

    private void OnDisable()
    {
        if (socket != null)
        {
            // Unsubscribe from the socket's events
            socket.selectEntered.RemoveListener(OnItemInserted);
            socket.selectExited.RemoveListener(OnItemRemoved);
        }
    }

    // Called when an item is placed in the socket
    private void OnItemInserted(SelectEnterEventArgs args)
    {
        UpdateCountText();
    }

    // Called when an item is removed from the socket
    private void OnItemRemoved(SelectExitEventArgs args)
    {
        RemoveCountText();
    }

    // Update the item count display
    private void UpdateCountText()
    {
        if (slot != null && itemText != null)
        {
            int itemCount = slot.GetItemCount();
            itemText.text = "X" + itemCount.ToString();
        }
    }

    // Remove the item count display
    private void RemoveCountText()
    {
        if (itemText != null)
        {
            itemText.text = "";
        }
    }
}
