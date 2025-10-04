using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerOffer : MonoBehaviour
{
    private SellableItem price;
    private ItemInventory count;

    private int hargaAsli;
    private int hargaTawarFix;

    // Ambil harga asli dari item di socket VR
    public void GetOfferPrice(SellableItem item)
    {
        if (item == null) return;

        price = item;
        hargaAsli = price.GetSellPrice();
        hargaTawarFix = hargaAsli; // defaultnya harga asli
        Debug.Log("Harga asli barang: " + hargaAsli);
    }

    // Fungsi untuk player menawar harga (pakai int)
    public bool TryNegotiate(int penawaranPlayer)
    {
        if (hargaAsli <= 0) return false;

        int minHarga = Mathf.RoundToInt(hargaAsli * 0.85f); // 15% lebih murah
        int maxHarga = Mathf.RoundToInt(hargaAsli * 0.95f); // 5% lebih murah

        Debug.Log($"Range tawar: {minHarga} - {maxHarga}, Penawaran: {penawaranPlayer}");

        if (penawaranPlayer >= minHarga && penawaranPlayer <= maxHarga)
        {
            // Penawaran diterima
            hargaTawarFix = penawaranPlayer;
            Debug.Log("Penawaran diterima! Harga fix: " + hargaTawarFix);
            return true;
        }
        else
        {
            // Penawaran ditolak, harga tetap asli
            hargaTawarFix = hargaAsli;
            Debug.Log("Penawaran ditolak. Harga tetap: " + hargaAsli);
            return false;
        }
    }

    // Ambil harga final setelah tawar menawar
    public int GetFinalPrice()
    {
        return hargaTawarFix;
    }
}
