using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class ItemInventory : MonoBehaviour
{
    // Prefab automatically assigned from the current GameObject (this item)
    public GameObject prefab;

    // Unique identifier for the item type
    public string itemID;

    // The count of items in the object, starting at 1 (default for single items)
    public int itemCount = 1; 

    // Reference to the external TextMeshProUGUI on the canvas
    public TextMeshProUGUI externalItemCountText;

    void Awake()
    {
        // Automatically assign the prefab to the same GameObject if it's not already assigned
        if (prefab == null)
        {
            prefab = gameObject;  // Assign the prefab to be the same as the current GameObject
        }

        // Assign the itemID based on the prefab name, removing any "(Clone)" or numbering
        itemID = CleanName(prefab.name);

        // Update the external text if assigned
        UpdateItemCountText();
    }

    // Returns the prefab assigned to this instance of ItemInventory
    public GameObject GetItemPrefab()
    {
        if (prefab != null)
        {
            return prefab;
        }
        else
        {
            Debug.LogWarning("Prefab not assigned for this item.");
            return null;
        }
    }

    // Helper method to clean the prefab name of any "(Clone)" or numbering suffixes like "(1)", "(2)", etc.
    private string CleanName(string name)
    {
        // Use regex to remove any pattern that matches "(...)" including "(Clone)" or "(1)", "(2)", etc.
        return Regex.Replace(name, @"\s*\(.*?\)$", "").Trim();
    }

    // Update the item count text on the external canvas
    public void UpdateItemCountText()
    {
        if (externalItemCountText != null && itemCount > 1)
        {
            externalItemCountText.text = "x" + itemCount.ToString();
        }
    }

}
