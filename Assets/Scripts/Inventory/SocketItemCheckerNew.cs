using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketItemCheckerNew : MonoBehaviour
{
    public XRSocketInteractor socket;       // Reference to the XR Socket Interactor
    public TextMeshProUGUI itemText;        // UI element for displaying the item count
    public SlotNew slot;                       // Reference to your Slot script

    private void OnEnable()
    {
        if (socket != null)
        {
            socket.selectEntered.AddListener(OnItemInserted);
            socket.selectExited.AddListener(OnItemRemoved);
        }
    }

    private void OnDisable()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnItemInserted);
            socket.selectExited.RemoveListener(OnItemRemoved);
        }
    }

    private void OnItemInserted(SelectEnterEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null) return;

        ItemInventory itemInventory = item.GetComponent<ItemInventory>();
        if (itemInventory == null) return;

        if (slot != null)
        {
            UpdateCountText();
        }
    }

    private void OnItemRemoved(SelectExitEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null) return;

        ItemInventory itemInventory = item.GetComponent<ItemInventory>();
        if (itemInventory == null) return;

        if (slot != null)
        {
            // Remove the item from the slot
            slot.RemoveItem(itemInventory);
            UpdateCountText();
        }
    }

    private void UpdateCountText()
    {
        if (slot != null && itemText != null)
        {
            int itemCount = slot.GetItemCount();
            itemText.text = itemCount > 0 ? "X" + itemCount.ToString() : "";
        }
    }
}

