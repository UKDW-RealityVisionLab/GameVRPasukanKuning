using UnityEngine;

public class Item : MonoBehaviour
{
    // Enum for item categories
    public enum ItemCategory
    {
        Organic,
        NonOrganic,
        Hazardous
    }

    // Enum for specific item types
    public enum ItemType
    {
        //organic
        Compost,
        //NonOrganic
        Glass,
        Metal,
        Plastic,
        //Hazard
        hazard
    }

    public ItemType itemType;     // Dropdown for item type
    public ItemCategory itemCategory; // Dropdown for item category

}
