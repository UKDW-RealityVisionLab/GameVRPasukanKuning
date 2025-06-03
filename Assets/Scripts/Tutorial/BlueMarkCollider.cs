using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMarkCollider : MonoBehaviour
{
    public GameObject transmuteUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transmuteUI != null)
            {
                transmuteUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            transmuteUI.SetActive(false);
        }
    }
}
