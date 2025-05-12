using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class Craft : MonoBehaviour
{
    public GameObject[] CraftPanel;  // Array to manually assign item inventory GameObjects
    public string craftedItemID;     // Item ID to be crafted
    private Dictionary<string, int> currentItems = new Dictionary<string, int>(); // Current available ingredients

    public Transform spawnLocation;  // Location to spawn the crafted item (e.g., in the player's inventory or in the world)
    public TextMeshProUGUI FeedbackCrafting; // Reference to UI FeedbackCrafting

    // Called when the user wants to craft an item
    public void CraftItem()
    {
        // Step 1: Gather ingredients from the CraftPanel GameObjects
        GatherIngredients();

        // Step 2: Determine what item can be crafted based on available ingredients
        craftedItemID = GetCraftableItemID();

        if (!string.IsNullOrEmpty(craftedItemID))
        {
            // If we have a valid crafted item ID, proceed to craft the item
            Debug.Log("Crafting item with ID: " + craftedItemID);
            // Proceed with item creation (e.g., adjust inventory, etc.)
            CreateCraftedItem();
            FeedbackCrafting.text = "Proses Crafting Berhasil";
            StartCoroutine(ClearFeedbackAfterDelay(2f));
        }
        else
        {
            Debug.Log("No valid item can be crafted from current ingredients.");
        }
    }

    // Step 1: Gather ingredients from the CraftPanel
    private void GatherIngredients()
    {
        currentItems.Clear();  // Clear the current ingredient list

        foreach (GameObject child in CraftPanel)
        {
            Slot Slot = child.GetComponent<Slot>();
            if (Slot != null)
            {
                string itemID = Slot.currentItemID;
                int itemCount = Slot.itemCount;

                if (currentItems.ContainsKey(itemID))
                {
                    currentItems[itemID] += itemCount;
                }
                else
                {
                    currentItems[itemID] = itemCount;
                }
            }
        }
    }

    // Step 2: Determine which item can be crafted based on the ingredients available
    private string GetCraftableItemID()
    {
        foreach (var recipe in RecipeManager.GetAllRecipes())  // Assuming GetAllRecipes returns a list of all recipes
        {
            string recipeItemID = recipe.Key;
            Dictionary<string, int> recipeIngredients = recipe.Value;

            bool canCraft = true;

            // Check if we have enough of each ingredient for this recipe
            foreach (var ingredient in recipeIngredients)
            {
                if (!currentItems.ContainsKey(ingredient.Key) || currentItems[ingredient.Key] < ingredient.Value)
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {
                return recipeItemID;  // Return the crafted item ID if all ingredients are available
            }
        }

        return null;  // No valid item can be crafted
    }

    // Create the crafted item (e.g., adjust inventory, destroy ingredients, etc.)
    private void CreateCraftedItem()
    {
        // Get the recipe for ingredients
        Dictionary<string, int> recipe = RecipeManager.GetRecipe(craftedItemID);

        if (recipe != null)
        {
            // Decrease the item count for each ingredient
            foreach (var ingredient in recipe)
            {
                foreach (GameObject child in CraftPanel)
                {
                    Slot Slot = child.GetComponent<Slot>();
                    if (Slot != null && Slot.currentItemID == ingredient.Key)
                    {
                        Slot.DecreaseItemCount(ingredient.Value);
                    }
                }
            }

            // Retrieve the prefabs associated with the crafted item
            RecipeManagerHolder.RecipeData recipeData = RecipeManagerHolder.GetRecipeData(craftedItemID);

            if (recipeData != null && recipeData.prefabs != null && recipeData.prefabs.Length > 0 && spawnLocation != null)
            {
                foreach (var prefab in recipeData.prefabs)
                {
                    Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);
                }
                Debug.Log("Crafted item spawned: " + craftedItemID);
            }
            else
            {
                Debug.LogError("No prefab assigned for crafted item: " + craftedItemID);
            }
        }
        else
        {
            Debug.LogError("Recipe not found for crafted item: " + craftedItemID);
        }
    }

    private IEnumerator ClearFeedbackAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FeedbackCrafting.text = "";
    }
}
