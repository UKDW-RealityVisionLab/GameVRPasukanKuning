using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour
{
    [System.Serializable]
    public struct AllowedItem
    {
        public Item.ItemCategory itemCategory; // Allowed item category
    }

    public AllowedItem[] allowedItems; 
    private Dictionary<Item.ItemType, int> itemCounts = new Dictionary<Item.ItemType, int>();

    private ScoreManager scoreManager;

    private void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        bool isAllowedItem = false;

        foreach (AllowedItem allowedItem in allowedItems)
        {
            if (item.itemCategory == allowedItem.itemCategory)
            {
                isAllowedItem = true;
                if (itemCounts.ContainsKey(item.itemType))
                {
                    itemCounts[item.itemType]++;
                }
                else
                {
                    itemCounts[item.itemType] = 1;
                }

                if (scoreManager != null)
                {
                    scoreManager.UpdateScore(2); // Correct item, add 2
                }
                Destroy(other.gameObject);
                
                return;
                
            }
        }

        if (!isAllowedItem && scoreManager != null)
        {
            scoreManager.UpdateScore(-1); // Incorrect item, subtract 1
        }
    }

    // Expose the collected item counts
    public Dictionary<Item.ItemType, int> GetCollectedItems()
    {
        // Log the collected items in a readable format
        foreach (var itemPair in itemCounts)
        {
            Debug.Log($"Collected {itemPair.Value} {itemPair.Key}"); // Log format
        }

        return new Dictionary<Item.ItemType, int>(itemCounts); // Return a copy of the dictionary
    }

    public void ClearCollectedItems()
    {
        itemCounts.Clear();
    }
}
