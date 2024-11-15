using System.Collections.Generic;

public static class RecipeManager
{
    // Example recipe dictionary
    private static Dictionary<string, Dictionary<string, int>> recipes = new Dictionary<string, Dictionary<string, int>>
    {
        { "Statue", new Dictionary<string, int> { { "Plastic", 2 }, { "Metal", 2 } } },
        { "Mirror", new Dictionary<string, int> { { "Metal", 2 }, { "Glass", 2 } } },
        { "Bag", new Dictionary<string, int> { { "Plastic", 2 }, { "Metal", 1 } } },
        { "Necklace", new Dictionary<string, int> { { "Plastic", 1 }, { "Metal", 1 } } },
        { "Can", new Dictionary<string, int> { { "Metal", 1 }} },
        { "Bottle", new Dictionary<string, int> { { "Plastic", 1 }} }
    };

    // Get the recipe for a given item ID (ingredients)
    public static Dictionary<string, int> GetRecipe(string itemID)
    {
        if (recipes.ContainsKey(itemID))
        {
            return recipes[itemID];
        }
        return null;
    }

    // Get all available recipes
    public static Dictionary<string, Dictionary<string, int>> GetAllRecipes()
    {
        return recipes;
    }
}
