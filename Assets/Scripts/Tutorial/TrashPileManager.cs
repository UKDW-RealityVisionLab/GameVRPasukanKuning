using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPileManager : MonoBehaviour
{
    public GameObject takeOutTrashUI;
    public GameObject blueMark;

    void Update()
    {
        CheckForActiveChildren();
    }

    void CheckForActiveChildren()
    {
        if (transform.childCount == 0)
        {
            if (takeOutTrashUI != null)
            {
                takeOutTrashUI.SetActive(true);
                blueMark.SetActive(true);
            }
        }
        else
        {
            bool anyChildActive = false;
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    anyChildActive = true;
                    break;
                }
            }

            if (!anyChildActive)
            {
                if (takeOutTrashUI != null)
                {
                    takeOutTrashUI.SetActive(true);
                    blueMark.SetActive(false);
                }
            }
            else
            {
                if (takeOutTrashUI != null)
                {
                    takeOutTrashUI.SetActive(false);
                }
            }
        }
    }
}
