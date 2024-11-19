using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryItemCheck : MonoBehaviour
{
    private int itemCount; // Number of items in the slot
    public TextMeshProUGUI ItemText; // UI element to display the count

    private ItemInventory itemInventory; // Reference to the item's inventory
    private Coroutine updateCoroutine; // Reference to the active coroutine

    // Called when an item is hovered over
    public void Hover(HoverEnterEventArgs args)
    {
        GameObject item = args.interactableObject?.transform?.gameObject;
        if (item == null) return;

        // Get the ItemInventory component from the hovered item
        itemInventory = item.GetComponent<ItemInventory>();
        if (itemInventory != null)
        {
            itemCount = itemInventory.itemCount; // Assign the item count from the hovered item

            // If a coroutine is already running, stop it before starting a new one
            if (updateCoroutine != null)
            {
                StopCoroutine(updateCoroutine);
            }

            // Start a new coroutine to delay the text update
            updateCoroutine = StartCoroutine(UpdateTextAfterDelay(2f)); // 1 second delay
        }
    }

    // Coroutine to update the item count text after a delay
    private IEnumerator UpdateTextAfterDelay(float delay)
    {
        // Wait for the specified time (delay)
        yield return new WaitForSeconds(delay);
        Debug.Log("Updateeeeee");
        // Update the UI text with the new item count
        UpdateCountText();
    }

    // Update the item count display
    private void UpdateCountText()
    {
        if (ItemText != null)
        {
            ItemText.text = "X" + itemCount.ToString(); // Set the item count text
        }
        else
        {
            Debug.LogError("ItemText is not assigned in the slot.");
        }
    }
}
