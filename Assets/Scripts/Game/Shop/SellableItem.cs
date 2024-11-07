using UnityEngine;

public class SellableItem : MonoBehaviour
{

    public MaterialCraft.CraftType craftType;
    public int sellPrice;

    public int GetSellPrice()
    {
        return sellPrice;
    }
}
