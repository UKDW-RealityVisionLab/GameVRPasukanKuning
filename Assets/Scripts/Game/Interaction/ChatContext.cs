using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatContext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHeader;
    [SerializeField] private TextMeshProUGUI textIsi;
    [SerializeField] private string headerString;
    [SerializeField] private AIBehaviour aiContext;
    [SerializeField] private TextAsset textContext;
    public NPCType Type;

    private JsonData dialogData;
    private List<string> currentDialogLines = new List<string>();
    private int currentLineIndex = 0;
    private bool isDialogActive = false;


    void Start()
    {
        dialogData = JsonMapper.ToObject(textContext.text);
        textHeader.text = headerString;
        textIsi.text = "Selamat datang!";
    }
    //private void Awake()
    //{
    //    LoadDialogData();
    //}
    //private void LoadDialogData()
    //{
    //    if (textContext == null)
    //    {
    //        Debug.LogError("Text context belum diset!");
    //        return;
    //    }

    //    dialogData = JsonMapper.ToObject(textContext.text);
    //}

    // Update is called once per frame
    void Update()
    {
        GetContext();
    }
    public void GetContext()
    {
        textHeader.text = headerString;
    }
    //public void SetContextExplanation()
    //{
    //    currentDialogLines = GetDialogList("explanation");
    //    StartChainedDialog(currentDialogLines);
    //}
    //public void SetContextGuidingPlayer()
    //{
    //    currentDialogLines = GetDialogList("information");
    //    StartChainedDialog(currentDialogLines);
    //}
    //public void SetContextUsualTalk()
    //{
    //    currentDialogLines = GetDialogList("questions"); // atau campur dengan answers
    //    StartChainedDialog(currentDialogLines);
    //}
    //public void SetContextOffer()
    //{
    //    currentDialogLines = GetDialogList("offer");
    //    StartChainedDialog(currentDialogLines);
    //}

    //private void StartChainedDialog(List<string> lines)
    //{
    //    if (lines == null || lines.Count == 0)
    //    {
    //        textIsi.text = "Tidak ada dialog tersedia.";
    //        return;
    //    }

    //    currentDialogLines = lines;
    //    currentLineIndex = 0;
    //    isDialogActive = true;
    //    ShowCurrentLine();
    //}

    //private void ShowCurrentLine()
    //{
    //    textIsi.text = currentDialogLines[currentLineIndex];
    //}

    //public void ContinueDialog()
    //{
    //    if (!isDialogActive) return;

    //    currentLineIndex++;

    //    if (currentLineIndex < currentDialogLines.Count)
    //    {
    //        ShowCurrentLine();
    //    }
    //    else
    //    {
    //        EndDialog();
    //    }
    //}
    //private void EndDialog()
    //{
    //    isDialogActive = false;
    //    textIsi.text = "Selesai berbicara.";
    //}
    //private List<string> GetDialogList(string type)
    //{
    //    List<string> results = new List<string>();

    //    if (dialogData == null || !dialogData.Keys.Contains(aiContext.roleAi)) return results;

    //    JsonData npcSection = dialogData[aiContext.roleAi];

    //    if (npcSection.Keys.Contains(type))
    //    {
    //        foreach (JsonData item in npcSection[type])
    //        {
    //            results.Add(item.ToString());
    //        }
    //    }

    //    return results;
    //}
}
