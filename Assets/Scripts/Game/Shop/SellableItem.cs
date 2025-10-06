using UnityEngine;

public class SellableItem : MonoBehaviour
{
    public MaterialCraft.CraftType craftType;
    public int sellPrice; // harga asli
    public int finalSellPrice; // harga hasil tawar (default = harga asli)

    void Awake()
    {
        finalSellPrice = sellPrice; // inisialisasi default
    }

    // Ambil harga asli
    public int GetSellPrice()
    {
        return finalSellPrice;
    }

    // Set harga tawar (dipanggil SellerOffer kalau tawarannya diterima)
    public void SetFinalPrice(int newPrice)
    {
        finalSellPrice = newPrice;
    }
}
