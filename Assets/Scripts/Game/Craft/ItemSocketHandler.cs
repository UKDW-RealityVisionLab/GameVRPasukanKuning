using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ItemSocketHandler : MonoBehaviour
{
    public Transform dropTarget; // Where items will be dropped
    private XRSocketInteractor socketInteractor; // Reference to the XR Socket Interactor
    private ItemMaterial itemMaterial; // Reference to the ItemSpawner script

    private void Start()
    {
        // Try to find XRSocketInteractor on this GameObject
        socketInteractor = GetComponent<XRSocketInteractor>();
        
        // Check if the socket interactor exists and subscribe to the event
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnObjectPlacedInSocket);
        }
        else
        {
            Debug.LogWarning("XRSocketInteractor component is missing from this object.");
        }
    }

    // This method is triggered when the object is placed in the socket
    private void OnObjectPlacedInSocket(SelectEnterEventArgs args)
    {
        // Get the ItemSpawner component from the object placed in the socket
        itemMaterial = (args.interactableObject as MonoBehaviour)?.GetComponent<ItemMaterial>();

        if (itemMaterial != null)
        {
            // Drop the item and destroy the placed object
            DropItemAndDestroy(args.interactableObject.transform.gameObject);
        }
        else
        {
            Debug.LogWarning("The object placed in the socket does not have an ItemSpawner component.");
        }
    }

    // Method to drop the item and destroy the object placed in the socket
    private void DropItemAndDestroy(GameObject placedObject)
    {
        if (dropTarget == null)
        {
            Debug.LogWarning("Drop target is missing.");
            return;
        }

        // Get the prefab from ItemSpawner and instantiate it
        GameObject prefab = itemMaterial.GetItemPrefab();
        if (prefab != null)
        {
            Instantiate(prefab, dropTarget.position, Quaternion.identity);
            Debug.Log($"Dropped {itemMaterial.itemPrefabPair.craftType} at {dropTarget.name}");

            // Destroy the object that was placed in the socket
            Destroy(placedObject);
        }
        else
        {
            Debug.LogWarning("No valid prefab to spawn.");
        }
    }

    // Cleanup: Remove the event listener when the object is destroyed
    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlacedInSocket);
        }
    }
}
