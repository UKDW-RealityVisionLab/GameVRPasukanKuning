using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    public string currentItemID; // Item ID of the first added item
    public int itemCount = 0; // Number of items in the slot
    public TextMeshProUGUI ItemText; // UI element to display the count

    private ItemInventory itemInventory; // Reference to the item's inventory
    private GameObject previousItem = null; // Last added item
    private Coroutine removeCoroutine = null; // Active removal coroutine
    private bool isBusy = false; // Unified flag for item addition/removal
    private bool isHovered = false; // Indicates hover state

    // Called when an item is hovered over
    public void Hover(HoverEnterEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null) return;

        ItemInventory hoveredItemInventory = item.GetComponent<ItemInventory>();
        if (hoveredItemInventory == null || !IsSameType(hoveredItemInventory)) return;

        isHovered = true;
        HandleItemAddition(item); // Add item on hover
        UpdateCountText();
    }

    // Handle addition of the item to the slot
    private void HandleItemAddition(GameObject item)
    {
        if (isBusy || previousItem != null && previousItem == item) return;

        itemInventory = item.GetComponent<ItemInventory>();

        if (itemInventory != null && IsSameType(itemInventory))
        {
            StartCoroutine(AddItemWithDelay(item));
        }

    }

    // Called when the hover exits
    public void HoverExited(HoverExitEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null || previousItem == null) return;

        ItemInventory exitedItemInventory = item.GetComponent<ItemInventory>();
        if (exitedItemInventory == null || !IsSameType(exitedItemInventory)) return;

        HandleItemRemoval(item); // Remove item on hover exit
    }

    // Handle item removal when hover exits
    private void HandleItemRemoval(GameObject item)
    {
        if (removeCoroutine != null)
        {
            StopCoroutine(removeCoroutine); // Stop any active removal coroutine
        }

        removeCoroutine = StartCoroutine(RemoveItemAfterDelay(item, 0.1f)); // Delay removal for smooth transition
    }

    // Add the item to the slot with a delay
    private IEnumerator AddItemWithDelay(GameObject item)
    {
        isBusy = true;

        // Destroy the previous item if it exists
        if (previousItem != null)
        {
            Destroy(previousItem);
        }

        // Add the new item to the slot
        itemCount += itemInventory.itemCount;
        previousItem = item;
        itemInventory.itemCount = itemCount;
        // UpdateCountText();

        yield return new WaitForSeconds(0); // Delay for smooth interaction
        isBusy = false;
    }

    // Remove the item after a short delay
    private IEnumerator RemoveItemAfterDelay(GameObject item, float delay)
    {
        isBusy = true;
        yield return new WaitForSeconds(delay);

        if (previousItem != null && previousItem == item)
        {
            itemCount = 0;

            if (itemCount == 0)
            {
                previousItem = null;
            }
        }
        

        isBusy = false;
    }


    // Check if the hovered item is the same type as the slot's current item
    private bool IsSameType(ItemInventory itemInventory)
    {
        if (itemCount == 0)
        {
            currentItemID = itemInventory.itemID; // Set the ID for the first item
            return true;
        }

        return itemInventory.itemID == currentItemID; // Check if the type matches
    }

    // Update the item count display
    public void UpdateCountText()
    {
        if (ItemText != null)
        {

            ItemText.text = "X" + itemCount.ToString();
            
            
        }
        else
        {
            Debug.LogError("ItemText is not assigned in the slot.");
        }
    }


    // Remove the item count display
    public void RemoveCountText()
    {
        if (ItemText != null)
        {
            ItemText.text = "";
        }
        else
        {
            Debug.LogError("ItemText is not assigned in the slot.");
        }
    }

        // Method to get the count of items in the slot
    public int GetItemCount()
    {
        return itemCount;
    }

    // Method to get the count of items in the slot
    public int DecreaseItemCount(int itemcount)
    {

        itemInventory.itemCount -= itemcount;
        itemCount = itemInventory.itemCount;
        // UpdateCountText();
        // Check if item count is now zero and destroy the GameObject if so
        if (itemCount <= 0)
        {
            Destroy(previousItem);
            // RemoveCountText();
        }
        return itemCount;
    }
}






