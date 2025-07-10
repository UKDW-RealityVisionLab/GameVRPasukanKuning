using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingUIActivator : MonoBehaviour
{
    public GameObject sellingUI;

    public void ActivateSellingUI()
    {
        // Ensure the craftingUI GameObject is assigned
        if (sellingUI != null)
        {
            sellingUI.SetActive(true);
        }
    }
}
