using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomingCollider : MonoBehaviour
{
    public GameObject welcomingUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (welcomingUI != null)
            {
                welcomingUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Welcoming Object belum diatur di Inspector!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (welcomingUI != null)
            {
                welcomingUI.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Welcoming Object belum diatur di Inspector!");
            }
        }
    }
}
