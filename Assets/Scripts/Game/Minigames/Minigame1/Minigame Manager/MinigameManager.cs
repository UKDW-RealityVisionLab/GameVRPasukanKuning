using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public NPCFSMController[] npcs;

    private int drownedCount = 0;

    void Start()
    {
        foreach (var npc in npcs)
        {
            npc.OnDrowned += HandleNPCDrowned;
        }
    }

    void HandleNPCDrowned()
    {
        drownedCount++;
        Debug.Log("NPC drowned. Total: " + drownedCount);

        if (drownedCount >= 3)
        {
            Debug.Log("All NPCs drowned!");
            // Trigger fail state, UI, etc.
        }
    }
}
