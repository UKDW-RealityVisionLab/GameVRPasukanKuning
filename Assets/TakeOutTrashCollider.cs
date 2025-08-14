using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOutTrashCollider : MonoBehaviour
{
    public GameObject takeOutTrashUI;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (takeOutTrashUI != null)
            {
                takeOutTrashUI.SetActive(true);
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