using UnityEngine;

[CreateAssetMenu(fileName = "ChatBubble", menuName = "Chat/Chat Bubble")]
public class ChatBubble3D : ScriptableObject
{
    public string id;
    public string displayName;
    [HideInInspector] public bool isComplete;
}
