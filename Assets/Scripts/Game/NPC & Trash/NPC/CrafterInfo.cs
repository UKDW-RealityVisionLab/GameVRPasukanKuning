using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterInfo : MonoBehaviour
{
    public GameObject[] craftInfoObject;
    private int currentIndex = 0;


    public void ResourceSetActive()
    {
        foreach (GameObject obj in craftInfoObject)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void ResourceSetOff()
    {
        foreach (GameObject obj in craftInfoObject)
        {
            if (obj != null)
                obj.SetActive(false);
        }

    }
    public void InfoOneByOneEveryClick()
    {
        if (craftInfoObject == null || craftInfoObject.Length == 0)
            return;

        // Matikan semua dulu
        foreach (GameObject obj in craftInfoObject)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Aktifkan hanya index saat ini
        if (craftInfoObject[currentIndex] != null)
            craftInfoObject[currentIndex].SetActive(true);

        // Update index untuk klik berikutnya (looping kembali ke 0)
        currentIndex = (currentIndex + 1) % craftInfoObject.Length;
    }
}
