using UnityEngine;

public class ItemMaterial : MonoBehaviour
{
    [System.Serializable]
    public struct ItemPrefabPair
    {
        public MaterialCraft.CraftType craftType;
        public GameObject prefab;
    }

    public ItemPrefabPair itemPrefabPair; // Stores what can be spawned

    // Returns the prefab for the specified craft type
    public GameObject GetItemPrefab()
    {
        if (itemPrefabPair.prefab != null)
        {
            return itemPrefabPair.prefab;
        }
        else
        {
            Debug.LogWarning("Prefab not assigned for the specified craft type.");
            return null;
        }
    }
}
