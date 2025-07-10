using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMarkActivator : MonoBehaviour
{
    public GameObject craftingMark; // Drag your Crafting UI GameObject here in the Inspector
    public GameObject sellingMark;
    public GameObject craftingUI;

    public void ActivateCraftingUI()
    {
        // Ensure the craftingUI GameObject is assigned
        if (craftingMark != null)
        {
            craftingMark.SetActive(true); // Activate the crafting UI
            sellingMark.SetActive(true);
            craftingUI.SetActive(true);
        }
    }
}
