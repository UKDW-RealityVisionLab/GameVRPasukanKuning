using UnityEngine;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    public NPCFSMController[] npcs;
    public TMP_Text drownedCounterText; // Assign in inspector

    private int drownedCount = 0;

    void Start()
    {
        foreach (var npc in npcs)
        {
            npc.OnDrowned += HandleNPCDrowned;
        }

        UpdateDrownedText();
    }

    void HandleNPCDrowned()
    {
        drownedCount++;
        Debug.Log("NPC drowned. Total: " + drownedCount);
        UpdateDrownedText();

        if (drownedCount >= 3)
        {
            Debug.Log("All NPCs drowned!");
            // TODO: Trigger fail state or show Game Over
        }
    }

    void UpdateDrownedText()
    {
        if (drownedCounterText != null)
            drownedCounterText.text = $"Drowned: {drownedCount} / {npcs.Length}";
    }
}
