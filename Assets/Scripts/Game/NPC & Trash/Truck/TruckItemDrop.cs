using System.Collections.Generic;
using UnityEngine;

public class TruckItemDrop : MonoBehaviour
{
    [System.Serializable]
    public struct ItemPrefabPair
    {
        public Item.ItemType itemType;  // The type of item
        public GameObject prefab;        // Corresponding prefab to instantiate
    }

    public List<ItemPrefabPair> itemPrefabsList = new List<ItemPrefabPair>(); // List of item-type/prefab pairs
    private Dictionary<Item.ItemType, GameObject> itemPrefabs = new Dictionary<Item.ItemType, GameObject>(); // Dictionary to store at runtime
    public Transform dropTarget; // Target where items will be dropped
    private TrashCollection trashCollection; // Reference to the TrashCollection

    private void Start()
    {
        // Initialize the dictionary from the list
        foreach (ItemPrefabPair pair in itemPrefabsList)
        {
            if (!itemPrefabs.ContainsKey(pair.itemType))
            {
                itemPrefabs.Add(pair.itemType, pair.prefab);
            }
        }

        // Find TrashCollection in the scene
        trashCollection = FindObjectOfType<TrashCollection>();
        if (trashCollection == null)
        {
            Debug.LogError("TrashCollection not found in the scene.");
        }
    }

    public void DropItems()
    {
        if (dropTarget == null || trashCollection == null)
        {
            Debug.LogWarning("Drop target or TrashCollection is missing.");
            return;
        }

        // Get the collected items
        Dictionary<Item.ItemType, int> itemsToDrop = trashCollection.GetCollectedItems();
        Debug.Log("Items to drop: " + itemsToDrop.Count);

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

        // Optionally clear collected items after dropping
        trashCollection.ClearCollectedItems();
    }
}
