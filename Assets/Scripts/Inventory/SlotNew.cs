using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotNew : MonoBehaviour
{
    private string currentItemID;
    private int itemCount = 0;
    private ItemInventory trackedItem;

    // Adds or merges the item into the slot
    public bool AddOrMergeItem(ItemInventory itemInventory)
    {
        if (itemInventory == null) return false;

        // If no items are in the slot, initialize it
        if (itemCount == 0)
        {
            currentItemID = itemInventory.itemID;
            trackedItem = itemInventory;
            itemCount += itemInventory.itemCount;
            // itemInventory.AssignSlot(this);  // Assign this slot to the item
            return true; // Successfully added
        }

        // If the item matches the current type, merge it
        if (itemInventory.itemID == currentItemID)
        {
            itemCount += itemInventory.itemCount;

            // Optionally destroy the newly placed item if there's already an item in the slot
            if (itemCount > 0)
            {
                Destroy(itemInventory.gameObject);
            }

            return true; // Merge occurred
        }

        // If types don't match, do not merge (optionally reject the item here)
        return false;
    }

    public void RemoveItem(ItemInventory itemInventory)
    {
        if (itemInventory == null || itemInventory.itemID != currentItemID) return;

        itemCount -= itemInventory.itemCount;
        // itemInventory.ClearSlot();

        if (itemCount <= 0)
        {
            ResetSlot();
        }
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    private void ResetSlot()
    {
        currentItemID = null;
        itemCount = 0;
        trackedItem = null;
    }
}






