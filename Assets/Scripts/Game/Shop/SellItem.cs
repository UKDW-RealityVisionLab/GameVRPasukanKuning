using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SellItem : MonoBehaviour
{
    public CurrencyManager currencyManager; // Reference to CurrencyManager to update currency
    private XRSocketInteractor socketInteractor; // Socket for placing items to sell

    private void Start()
    {
        // Find XRSocketInteractor on this GameObject
        socketInteractor = GetComponent<XRSocketInteractor>();
        
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnItemPlacedInSocket);
        }
        else
        {
            Debug.LogWarning("XRSocketInteractor component is missing from this object.");
        }

        // Verify that CurrencyManager is assigned
        if (currencyManager == null)
        {
            Debug.LogWarning("CurrencyManager reference is missing.");
        }
    }

    // Called when an item is placed in the socket
    private void OnItemPlacedInSocket(SelectEnterEventArgs args)
    {
        // Try to get SellableItem component from the placed object
        SellableItem sellableItem = (args.interactableObject as MonoBehaviour)?.GetComponent<SellableItem>();

        if (sellableItem != null && currencyManager != null)
        {
            int sellPrice = sellableItem.GetSellPrice();

            // Add the sell price to the player's currency
            currencyManager.UpdateKoin(sellPrice);
            Debug.Log($"Sold {sellableItem.craftType} for {sellPrice} Koin.");

            // Destroy the sold item
            Destroy(args.interactableObject.transform.gameObject);
        }
        else
        {
            Debug.LogWarning("The object placed in the socket does not have a SellableItem component or CurrencyManager is missing.");
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnItemPlacedInSocket);
        }
    }
}
