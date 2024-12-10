using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using TMPro; // For TextMeshPro
using System.Collections;

public class SellItemButton : MonoBehaviour
{
    public CurrencyManager currencyManager; // Reference to CurrencyManager to update currency
    public TextMeshProUGUI sellPriceText; // Reference to a TextMeshProUGUI for displaying the sell price
    private XRSocketInteractor socketInteractor; // Socket for placing items to sell

    private void Start()
    {
        // Find XRSocketInteractor on this GameObject
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        if (socketInteractor == null)
        {
            Debug.LogWarning("XRSocketInteractor component is missing from this object.");
        }

        // Verify that CurrencyManager is assigned
        if (currencyManager == null)
        {
            Debug.LogWarning("CurrencyManager reference is missing.");
        }

        // Verify that sellPriceText is assigned
        if (sellPriceText == null)
        {
            Debug.LogWarning("Sell price text reference is missing.");
        }
    }

    // Method to sell the item currently in the socket
    public void SellItemInSocket()
    {
        if (socketInteractor != null && socketInteractor.hasSelection)
        {
            // Get the interactable object in the socket
            var interactableObject = socketInteractor.firstInteractableSelected;

            if (interactableObject != null)
            {
                // Try to get SellableItem component from the placed object
                SellableItem sellableItem = (interactableObject as MonoBehaviour)?.GetComponent<SellableItem>();
                ItemInventory itemInventory = (interactableObject as MonoBehaviour)?.GetComponent<ItemInventory>();

                if (sellableItem != null && itemInventory != null && currencyManager != null)
                {
                    int itemCount = itemInventory.itemCount; // Get the total count of items
                    int sellPrice = sellableItem.GetSellPrice();
                    int totalSellPrice = sellPrice * itemCount; // Calculate total sell price

                    // Add the sell price to the player's currency
                     currencyManager.UpdateKoin(totalSellPrice);
                    Debug.Log($"Sold {itemCount} x {sellableItem.craftType} for {totalSellPrice} Koin.");

                    // Show the sell price for a short time
                    ShowKoinChange(totalSellPrice);

                    // Destroy the sold item
                    Destroy(interactableObject.transform.gameObject);
                }
                else
                {
                    Debug.LogWarning("The object in the socket does not have a SellableItem component or CurrencyManager is missing.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No item in the socket to sell.");
        }
    }

    // Show the coin change above the trashcan for a brief period
    private void ShowKoinChange(int scoreChange)
    {
        // Set the text to show the coin change
        sellPriceText.text = (scoreChange > 0 ? "+" : "") + scoreChange + " Koin";

        // Start a coroutine to hide the text after a brief delay
        StartCoroutine(HideKoinChange(1f));
    }

    // Coroutine to hide the text after a delay
    private IEnumerator HideKoinChange(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Hide the sell price text by setting it to an empty string
        sellPriceText.text = "";
    }
}
