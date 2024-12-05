using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketItemChecker : MonoBehaviour
{
    public XRSocketInteractor socket;       // Reference to the XR Socket Interactor
    public TextMeshProUGUI itemText;        // UI element for displaying the item count
    public Slot slot;                       // Reference to your Slot script
    private ItemInventory itemInventory; // Reference to the item's inventory

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

        itemInventory = item.GetComponent<ItemInventory>();
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

        itemInventory = item.GetComponent<ItemInventory>();
        if (itemInventory == null) return;

        if (slot != null)
        {
            RemoveCountText();
        }
    }


        // Update the item count display
    public void UpdateCountText()
    {
        if (slot != null && itemText != null)
        {
            int itemCount = slot.GetItemCount();
            if (itemInventory.itemCount>0)
            {
                itemText.text = "X" + itemInventory.itemCount.ToString();
            }
            else
            {
                itemText.text = "";
            }            
            
        }
        else
        {
            Debug.LogError("ItemText is not assigned in the slot.");
        }
    }


    // Remove the item count display
    public void RemoveCountText()
    {
        if (itemText != null)
        {
            itemText.text = "";
        }
        else
        {
            Debug.LogError("ItemText is not assigned in the slot.");
        }
    }
}
