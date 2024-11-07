using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    [System.Serializable]
    public struct ItemPrefabPair
    {
        public Item.ItemType itemType;
        public GameObject prefab;
    }

    public List<ItemPrefabPair> itemPrefabsList = new List<ItemPrefabPair>(); // List of item-type/prefab pairs
    private Dictionary<Item.ItemType, GameObject> itemPrefabs = new Dictionary<Item.ItemType, GameObject>(); // Dictionary to store at runtime
    private TrashCollection trashCollection; // Reference to the TrashCollection component

    public Transform dropTarget; // Target where items will be dropped

    private void Start()
    {
        // Initialize the dictionary from the item-prefab list
        foreach (ItemPrefabPair pair in itemPrefabsList)
        {
            if (!itemPrefabs.ContainsKey(pair.itemType))
            {
                itemPrefabs.Add(pair.itemType, pair.prefab);
            }
        }

        // Get the TrashCollection component from the same GameObject
        trashCollection = GetComponent<TrashCollection>();
        if (trashCollection == null)
        {
            Debug.LogError("TrashCollection not found on the same GameObject.");
        }
    }

    // Method to drop items
    public void DropItems()
    {
        if (dropTarget == null || trashCollection == null)
        {
            Debug.LogWarning("Drop target or TrashCollection is missing.");
            return;
        }

        // Get the collected items from TrashCollection
        Dictionary<Item.ItemType, int> itemsToDrop = trashCollection.GetCollectedItems();
        Debug.Log("Items to drop: " + itemsToDrop);

        // Instantiate items based on counts
        foreach (var itemPair in itemsToDrop)
        {
            if (itemPrefabs.TryGetValue(itemPair.Key, out GameObject itemPrefab))
            {
                for (int i = 0; i < itemPair.Value; i++)
                {
                    Instantiate(itemPrefab, dropTarget.position, Quaternion.identity);
                    Debug.Log($"Dropped {itemPair.Key} at {dropTarget.name}");
                }
            }
            else
            {
                Debug.LogWarning($"No prefab found for item type: {itemPair.Key}");
            }
        }

        // Clear collected items after dropping
        trashCollection.ClearCollectedItems();
    }
}
