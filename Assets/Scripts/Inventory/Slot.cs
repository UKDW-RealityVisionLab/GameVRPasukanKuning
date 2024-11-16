using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    // This stores the itemID of the first item added to the slot
    public string currentItemID;
    private ItemInventory itemInventory;
    public int itemCount = 0; // Counter for the number of items in the slot
    private bool isHovered; // Counter for the number of items in the slot
    private GameObject previousItem = null; // Track the previous item in the slot
    private Coroutine removeCoroutine; // Track the remove coroutine

    private bool isAddingItem = false; // Flag to prevent duplicates

    private bool isRemovingItem = false; // Flag to lock slot state during item removal

    public TextMeshProUGUI ItemText;  // Reference to the Text component that will display the currency


    public void Hover(HoverEnterEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null) return;

        Debug.Log($"Hovering over item: {item.name}");

        ItemInventory hoveredItemInventory = item.GetComponent<ItemInventory>();
        isHovered = true;

        if (hoveredItemInventory == null || !IsSameType(hoveredItemInventory)) return;

        HandlePreviousItemDeactivation(item);
    }

    private void HandlePreviousItemDeactivation(GameObject currentItem)
    {
        if (previousItem != null && previousItem != currentItem && isHovered)
        {
            previousItem.SetActive(false);
            Debug.Log($"Deactivated previous item: {previousItem.name}");
        }
    }

    public void HoverExited(HoverExitEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null) return;

        Debug.Log($"Hover exited for item: {item.name}");

        ItemInventory exitedItemInventory = item.GetComponent<ItemInventory>();
        if (exitedItemInventory == null || !IsSameType(exitedItemInventory) || previousItem == null) return;

        ReactivatePreviousItem();
    }

    private void ReactivatePreviousItem()
    {
        previousItem.SetActive(true);
        Debug.Log($"Reactivated previous item: {previousItem.name}");
        isHovered = false;
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
        yield return new WaitForSeconds(0.5f);

        // Reset the flag after delay
        isAddingItem = false;
    }

    // Called when an item is removed (deselected) from the slot
// Called when an item is removed (deselected) from the slot
public void OnItemRemoved(SelectExitEventArgs interactable)
{
    GameObject item = interactable.interactableObject.transform.gameObject;

    // If we're already in the process of removing an item, return early
    if (isRemovingItem)
    {
        Debug.Log("Removal already in progress. Skipping removal for item: " + item.name);
        return;
    }

    Debug.Log("Item removed from Slot: " + item.name);

    // Start coroutine to wait for the specified delay before handling the item removal
    removeCoroutine = StartCoroutine(RemoveItemAfterDelay(item, 0.5f)); // 5 seconds delay
}

// Coroutine to handle item removal after a delay
private IEnumerator RemoveItemAfterDelay(GameObject item, float delay)
{
    isRemovingItem = true; // Lock the slot state to prevent concurrent removals

    yield return new WaitForSeconds(delay); // Wait for the specified delay

    // Ensure the item to be removed is still valid and no new item has replaced it
    if (previousItem != null && previousItem == item)
    {
        Debug.Log("Item removal confirmed after delay.");

        // Decrease the count of items in the slot
        itemCount = 0;
        UpdatecountText();

        // If the slot is empty, handle cleanup
        if (itemCount == 0)
        {
            Debug.Log("Slot is now empty. Clearing item reference.");
            RemovecountText();

            // Clear the reference to the previous item, but do not destroy it
            previousItem = null;
        }
        else
        {
            Debug.Log("Slot still contains items. Item count: " + itemCount);
        }
    }
    else
    {
        Debug.Log("Item removal skipped. A new item may have been added or the item is no longer valid.");
    }

    isRemovingItem = false; // Unlock the slot state
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

    // Method to get the count of items in the slot
    public int DecreaseItemCount(int itemcount)
    {

        itemInventory.itemCount -= itemcount;
        itemCount = itemInventory.itemCount;
        UpdatecountText();
        // Check if item count is now zero and destroy the GameObject if so
        if (itemCount <= 0)
        {
            Destroy(previousItem);
            RemovecountText();
        }
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
