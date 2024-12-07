using UnityEngine;
using System.Collections.Generic;

public class RecipeManagerHolder : MonoBehaviour
{
    [System.Serializable]
    public class RecipeData
    {
        public string itemName;  // Name of the item
        public GameObject[] prefabs;  // Prefabs for the item
    }

    // Store all recipes and their prefabs
    public RecipeData[] recipes;

    // Dictionary for quick access to recipe data based on item name
    private static Dictionary<string, RecipeData> recipeDataDictionary;

    void Awake()
    {
        recipeDataDictionary = new Dictionary<string, RecipeData>();

        // Initialize the dictionary with recipe data
        foreach (var recipe in recipes)
        {
            recipeDataDictionary[recipe.itemName] = recipe;
        }
    }

    // Get the recipe data by item name
    public static RecipeData GetRecipeData(string itemID)
    {
        if (recipeDataDictionary.ContainsKey(itemID))
        {
            return recipeDataDictionary[itemID];
        }
        return null;
    }
}
