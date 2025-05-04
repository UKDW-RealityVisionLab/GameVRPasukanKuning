using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    public static bool playerInZone = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDetection.playerInZone = true;

            // Find the NPC and set flag
            var npc = FindObjectOfType<NPCSellingBehaviour>();
            if (npc != null)
            {
                npc.SetGoToSellNext();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            }
        }
}

