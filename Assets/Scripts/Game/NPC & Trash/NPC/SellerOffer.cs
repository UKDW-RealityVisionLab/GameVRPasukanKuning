using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerOffer : MonoBehaviour
{
    private SellableItem price;
    private ItemInventory count;

    public void GetOfferPrice()
    {
        price.GetSellPrice();
        //count.itemCount;
    }
}
