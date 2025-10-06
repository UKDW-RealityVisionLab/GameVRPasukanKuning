using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerOffer : MonoBehaviour
{
    private SellableItem price;
    [SerializeField] private NPCInteractable npcInterac;
    [SerializeField] private SliderScript tawaran;

    private int hargaAsli;
    private int hargaTawarFix;
    public bool isDeal { get; private set; } // hasil tawar true/false

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
    public void TryNegotiate()
    {
        if (price == null || tawaran == null)
        {
            Debug.LogWarning("Price atau tawaran belum di-assign!");
            return;
        }
        int penawaranPlayer = hargaAsli + tawaran.GetHargaTambah();
        int minHarga = Mathf.RoundToInt(hargaAsli);
        int maxHarga = Mathf.RoundToInt(hargaAsli / Random.Range(0.85f, 0.95f));

        Debug.Log($"Range tawar: {minHarga} - {maxHarga}, Penawaran: {penawaranPlayer}");

        if (penawaranPlayer >= minHarga && penawaranPlayer <= maxHarga)
        {
            // Penawaran diterima
            hargaTawarFix = penawaranPlayer;
            price.SetFinalPrice(hargaTawarFix);
            Debug.Log("Penawaran diterima! Harga fix: " + hargaTawarFix);
            isDeal = true;
        }
        else
        {
            // Penawaran ditolak, harga tetap asli
            hargaTawarFix = hargaAsli;
            price.SetFinalPrice(hargaTawarFix);
            Debug.Log("Penawaran ditolak. Harga tetap: " + hargaAsli);
            isDeal = false;
        }
    }

    // Ambil harga final setelah tawar menawar
    public int GetFinalPrice()
    {
        return hargaTawarFix;
    }

    public void GetDealButton()
    {
        if(isDeal == true)
        {
            npcInterac.GuideButtonContext("Deal");
        }
        else
        {
            npcInterac.GuideButtonContext("Not_deal");
        }
    }
}
