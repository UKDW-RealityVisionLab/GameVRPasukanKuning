using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;  // Make sure you have this for TextMeshPro support

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

    public TextMeshProUGUI scoreText; // Reference to the UI TextMeshPro component for score display


    private void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText reference is missing.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        bool isAllowedItem = false;

        // Handle the allowed items
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
                    ShowScoreChange(2);
                }
                Destroy(other.gameObject); // Destroy the item after processing
                return;
            }
        }

        // Handle incorrect items
        if (!isAllowedItem && scoreManager != null)
        {
            scoreManager.UpdateScore(-1); // Incorrect item, subtract 1
            ShowScoreChange(-1);
        }
    }

    // Show the score change above the trashcan for a brief period
    private void ShowScoreChange(int scoreChange)
    {
        // Set the text to show the score change
        scoreText.text = (scoreChange > 0 ? "+" : "") + scoreChange;

        // Start a coroutine to hide the score change after a brief delay
        StartCoroutine(HideScoreChange(1f));
    }

    // Coroutine to remove the item after a delay
    private IEnumerator HideScoreChange(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay (5 seconds)
        
        // Hide the score text by setting it to an empty string
        scoreText.text = "";
    }

    private void HideScoreChange()

    {

        
    }

    // Expose the collected item counts
    public Dictionary<Item.ItemType, int> GetCollectedItems()
    {
        // Log the collected items in a readable format
        foreach (var itemPair in itemCounts)
        {
            Debug.Log($"Collected {itemPair.Value} {itemPair.Key}");
        }

        return new Dictionary<Item.ItemType, int>(itemCounts); // Return a copy of the dictionary
    }

    public void ClearCollectedItems()
    {
        itemCounts.Clear();
    }
}
