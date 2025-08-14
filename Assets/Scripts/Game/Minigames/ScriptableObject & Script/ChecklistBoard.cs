using UnityEngine;
using TMPro;

public class ChecklistBoard : MonoBehaviour
{
    public ChecklistDatabase checklistDatabase;
    public TMP_Text[] checklistTexts; // Drag your TMP text boxes here in inspector, order matters

    private void Start()
    {
        RefreshBoard();
    }

    public void RefreshBoard()
    {
        var items = checklistDatabase.GetAllItems();

        int count = Mathf.Min(items.Count, checklistTexts.Length);

        for (int i = 0; i < count; i++)
        {
            var item = items[i];
            checklistTexts[i].text = $"{item.displayName} {(item.isComplete ? "Complete" : "Incomplete")}";
        }

        // Optional: Clear any leftover TMP texts if you have more text boxes than items
        for (int i = count; i < checklistTexts.Length; i++)
        {
            checklistTexts[i].text = "";
        }
    }
}
