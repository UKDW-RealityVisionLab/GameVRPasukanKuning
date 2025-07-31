using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class NPCDialogLoader : MonoBehaviour
{
    public Dictionary<string, NPCDialogEntry> dialogData;

    void Awake()
    {
        LoadDialogFromJson();
    }

    void LoadDialogFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("npc_dialog");

        if (jsonFile == null)
        {
            Debug.LogError("File JSON tidak ditemukan di Resources!");
            return;
        }

        dialogData = JsonConvert.DeserializeObject<Dictionary<string, NPCDialogEntry>>(jsonFile.text);
    }

    public string GetRandomLine(string role, string category)
    {
        if (!dialogData.ContainsKey(role)) return $"[Tidak ada role: {role}]";

        NPCDialogEntry entry = dialogData[role];
        string[] lines = null;

        switch (category)
        {
            case "explanation": lines = entry.explanation; break;
            case "offer": lines = entry.offer; break;
            case "questions": lines = entry.questions; break;
            case "answers": lines = entry.answers; break;
            case "randomChat": lines = entry.randomChat; break;
            default: return $"[Kategori tidak dikenal: {category}]";
        }

        if (lines == null || lines.Length == 0) return "[Kosong]";
        return lines[Random.Range(0, lines.Length)];
    }
}
