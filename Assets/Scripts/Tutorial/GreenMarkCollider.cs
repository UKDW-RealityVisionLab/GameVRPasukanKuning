using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMarkCollider : MonoBehaviour
{
    public GameObject pickUpTrashUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickUpTrashUI != null)
            {
                pickUpTrashUI.SetActive(true);
                Debug.Log("Pick Up Trash UI berhasil diaktifkan.");
            }
            else
            {
                Debug.LogWarning("Pick Up Trash UI belum diatur di Inspector pada " + gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickUpTrashUI != null)
            {
                pickUpTrashUI.SetActive(false);
                Debug.Log("Pick Up Trash UI berhasil diaktifkan.");
            }
            else
            {
                Debug.LogWarning("Pick Up Trash UI belum diatur di Inspector pada " + gameObject.name);
            }
            gameObject.SetActive(false);
            Debug.Log(gameObject.name + " telah dinonaktifkan.");
        }
    }
}
