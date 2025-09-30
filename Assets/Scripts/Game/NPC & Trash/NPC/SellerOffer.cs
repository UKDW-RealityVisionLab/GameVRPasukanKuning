using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerOffer : MonoBehaviour
{
    private SellableItem price;
    private ItemInventory count;

    private float hargaAsli;
    private float hargaTawarFix;


    public void GetOfferPrice()
    {
        price.GetSellPrice();
        //count.itemCount;
    }
}
