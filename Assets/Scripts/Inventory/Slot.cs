using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    // This stores the itemID of the first item added to the slot
    private string currentItemID;
    private ItemInventory itemInventory;
    private int itemCount = 0; // Counter for the number of items in the slot
    private bool isHovered; // Counter for the number of items in the slot
    private GameObject previousItem = null; // Track the previous item in the slot
    private Coroutine removeCoroutine; // Track the remove coroutine

    private bool isAddingItem = false; // Flag to prevent duplicates

    public TextMeshProUGUI ItemText;  // Reference to the Text component that will display the currency


    public void Hover(HoverEnterEventArgs args)
    {
        GameObject item = args.interactableObject.transform.gameObject;
        ItemInventory hoveredItemInventory = item.GetComponent<ItemInventory>();

        // Ensure the new item hovering is recognized and matches the current item type
        Debug.Log("Hovering over item: " + item.name);
        isHovered = true;

        if (hoveredItemInventory != null && IsSameType(hoveredItemInventory))
        {
            // If there's a previous item of the same type, deactivate it only if it's not the one being added
            if (previousItem != null && previousItem != item && isHovered)
            {
                previousItem.SetActive(false);
                Debug.Log("Deactivated previous item: " + previousItem.name);
            }
        }
    }

    public void HoverExited(HoverExitEventArgs args)
    {
        GameObject item = args.interactableObject.transform.gameObject;
        ItemInventory exitedItemInventory = item.GetComponent<ItemInventory>();

        Debug.Log("Hover exited for item: " + item.name);

        // Reactivate the previous item only if it matches the type in the slot and hasn't been replaced
        if (exitedItemInventory != null && IsSameType(exitedItemInventory) && previousItem != null && isHovered)
        {
            previousItem.SetActive(true);
            Debug.Log("Reactivated previous item: " + previousItem.name);
            isHovered = false;
        }
    }


    // Called when an item is added to the socket
    public void OnItemAdded(SelectEnterEventArgs interactable)
    {
        // If we're already in the process of adding an item, return early to prevent duplicates
        if (isAddingItem)
            return;

        GameObject item = interactable.interactableObject.transform.gameObject;
        itemInventory = item.GetComponent<ItemInventory>();

        if (itemInventory != null)
        {
            // Check if the item is of the same type using itemID in ItemInventory
            if (IsSameType(itemInventory))
            {
                // Start a coroutine to handle item addition with a delay
                StartCoroutine(AddItemWithDelay(item));
            }
            else
            {
                Debug.Log("Cannot add a different type of item to this slot.");
            }
        }
        else
        {
            Debug.LogWarning("Item does not have an ItemInventory component.");
        }
    }

        private IEnumerator AddItemWithDelay(GameObject item)
    {
        isAddingItem = true; // Set flag to prevent duplicate additions

        // Update item count
        itemCount += itemInventory.itemCount;

        // Destroy previous item if applicable and not hovered
        if (previousItem != null && !isHovered)
        {
            Destroy(previousItem);
            Debug.Log("Destroyed previous item: " + previousItem.name);
        }

        Debug.Log("Item added to Socket.");

        // Update references
        previousItem = item;
        itemInventory.itemCount = itemCount;
        UpdatecountText();

        // Log the updated item count
        Debug.Log("Item added to slot. Current count: " + itemCount);

        // Wait for 1 second before allowing another item addition
        yield return new WaitForSeconds(1f);

        // Reset the flag after delay
        isAddingItem = false;
    }



        // Called when an item is removed (deselected) from the slot
    // Called when an item is removed (deselected) from the slot
    public void OnItemRemoved(SelectExitEventArgs interactable)
    {
        GameObject item = interactable.interactableObject.transform.gameObject;
        Debug.Log("Item removed from Slot: " + item.name);
        

        // Start coroutine to wait for 5 seconds before removing the item from the slot
        removeCoroutine = StartCoroutine(RemoveItemAfterDelay(item, 1f)); // 5 seconds delay
    }

    // Coroutine to remove the item after a delay
    private IEnumerator RemoveItemAfterDelay(GameObject item, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay (5 seconds)
        
        // Check if the item is still the one that was removed and if no other item has been added
        if (previousItem != null && previousItem == item)
        {
            Debug.Log("Item removed after delay.");

            // Decrease the count of the items in the slot
            itemCount = 0;
            RemovecountText();
            // If no items are left, destroy the previous item
            if (itemCount == 0 && previousItem != null)
            {
                // Destroy(previousItem);
                // Debug.Log("Destroyed last item from slot.");
                previousItem = null;
            }

            // Optionally log the updated count
            Debug.Log("Item count after removal: " + itemCount);
        }
        else
        {
            Debug.Log("Item not in slot or a new item was added before the delay, skipping removal.");
        }
    }

    // Check if the item is of the same type based on itemID in ItemInventory
    private bool IsSameType(ItemInventory itemInventory)
    {
        if (itemCount == 0)
        {
            // Set currentItemID based on the first item added to the slot
            currentItemID = itemInventory.itemID;
            return true;
        }

        // Compare itemID with current itemID in the slot
        return itemInventory.itemID == currentItemID;
    }

    // Method to get the count of items in the slot
    public int GetItemCount()
    {
        return itemCount;
    }

    private void UpdatecountText()
    {
        if (ItemText != null)
        {
            ItemText.text = "X" + itemCount.ToString();
        }
        else
        {
            Debug.LogError("item Text is not assigned in the Inventory.");
        }
    }

        private void RemovecountText()
    {
        if (ItemText != null)
        {
            ItemText.text = "";
        }
        else
        {
            Debug.LogError("item Text is not assigned in the Inventory.");
        }
    }
}
