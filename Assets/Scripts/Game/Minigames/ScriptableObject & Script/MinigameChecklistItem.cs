    using UnityEngine;

[CreateAssetMenu(fileName = "MinigameChecklistItem", menuName = "Checklist/Minigame Item")]
public class MinigameChecklistItem : ScriptableObject
{
    public string id;
    public string displayName;
    [HideInInspector] public bool isComplete;
}
