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
    public TextMeshProUGUI Feedback; // Reference to the UI TextMeshPro component for Feedback


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
        if (Feedback == null)
        {
            Debug.LogError("Feedback Text reference is missing.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        // Check if the item has an ItemInventory component
        ItemInventory itemInventory = other.GetComponent<ItemInventory>();
        if (itemInventory != null && itemInventory.currentSlot != null) return; // Exit if currentSlot is not null
        int countToAdd = (itemInventory != null) ? itemInventory.itemCount : 1; // Default to 1 if no inventory
        

        bool isAllowedItem = false;

        // Handle the allowed items
        foreach (AllowedItem allowedItem in allowedItems)
        {
            if (item.itemCategory == allowedItem.itemCategory)
            {
                isAllowedItem = true;

                // Add the item's count to the dictionary
                if (itemCounts.ContainsKey(item.itemType))
                {
                    itemCounts[item.itemType] += countToAdd;
                }
                else
                {
                    itemCounts[item.itemType] = countToAdd;
                }

                // Update the score based on the total count added
                if (scoreManager != null)
                {
                    int scoreToAdd = 2 * countToAdd; // Multiply by 2 for each item
                    scoreManager.UpdateScore(scoreToAdd);
                    Feedback.text = "Selamat! Kamu benar memasukkan sampah sesuai jenisnya";
                    ShowScoreChange(scoreToAdd);
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
            Feedback.text = "Oops! Itu bukan jenis sampah yang tepat";
            StartCoroutine(HideFeedback(2f));
        }
    }


    // Show the score change above the trashcan for a brief period
    private void ShowScoreChange(int scoreChange)
    {
        // Set the text to show the score change
        scoreText.text = (scoreChange > 0 ? "+" : "") + scoreChange;

        // Start a coroutine to hide the score change after a brief delay
        StartCoroutine(HideScoreChange(1f));
        StartCoroutine(HideFeedback(2f));
    }

    // Coroutine to remove the item after a delay
    private IEnumerator HideScoreChange(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay (5 seconds)

        // Hide the score text by setting it to an empty string
        scoreText.text = "";
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

    private IEnumerator HideFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        Feedback.text = "";
    }

}
