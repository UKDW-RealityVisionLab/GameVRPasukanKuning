using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChecklistDatabase", menuName = "Checklist/Checklist Database")]
public class ChecklistDatabase : ScriptableObject
{
    public List<MinigameChecklistItem> checklistItems;

    public void MarkComplete(string id)
    {
        var item = checklistItems.Find(x => x.id == id);
        if (item != null && !item.isComplete)
        {
            item.isComplete = true;
            Debug.Log($"✅ Minigame '{item.displayName}' marked as complete!");
        }
        else if (item == null)
        {
            Debug.LogWarning($"❌ No minigame with ID '{id}' found.");
        }
    }

    public List<MinigameChecklistItem> GetAllItems() => checklistItems;
}
