using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using System.Diagnostics.Tracing;
using UnityEditor.Rendering;
using System.Net.Http;
using System.Threading.Tasks;

public class ChatContext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHeader;
    [SerializeField] private TextMeshProUGUI textIsi;
    [SerializeField] private string headerString;
    [SerializeField] private TextAsset textContext;
    private NPCType type;
    public GameObject npcDialogUI;
    public TextMeshProUGUI dialogRandomText;
    private AIBehaviour ai;
    private NPCInteractable aiContext;
    [SerializeField] private float chatInterval = 10f; // tiap berapa detik muncul dialog
    private float chatTimer = 0f;
    private ListContext listCon;

    private JsonData dialogData;


    [SerializeField] private string roleAi = "Seller"; // Atur dari Inspector
    void Start()
    {
        textHeader.text = headerString;
    }

    // Update is called once per frame
    void Update()
    {
        GetContextQuestion();
        GetRandomChat();
        chatTimer += Time.deltaTime;

        if (chatTimer >= chatInterval)
        {
            //ShowRandomDailyChat();
            chatTimer = 0f;
        }
    }

    public void GetContextQuestion()
    {
        textHeader.text = headerString;
        textIsi.text = listCon.GetQuestion(ai)[0];
    }
    public void GetRandomChat()
    {
        ShowNPCDialog(listCon.GetRandomChat(ai)[0]);
    }

    public void NextButton()
    {
        textIsi.text = "";
    }
    public void ShowNPCDialog(string message, float duration = 5f)
    {
        if (npcDialogUI == null || dialogRandomText == null) return;

        npcDialogUI.SetActive(true);
        dialogRandomText.text = message;

        StartCoroutine(HideDialogAfterDelay(duration));
    }

    IEnumerator HideDialogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        npcDialogUI.SetActive(false);
    }
    //public void GetRandomChat(string category)
    //{
    //    if (dialogData == null || !dialogData.Keys.Contains(roleAi)) return;

    //    var roleData = dialogData[roleAi];

    //    if (!roleData.Keys.Contains(category)) return;

    //    JsonData lines = roleData[category];
    //    if (lines.Count == 0) return;

    //    int randomIndex = Random.Range(0, lines.Count);
    //    string message = lines[randomIndex].ToString();
    //    ShowNPCDialog(message);
    //}
    //public void ShowRandomDailyChat()
    //{
    //    GetRandomChat("randomChat"); // kategori baru seperti "randomChat"
    //}
}

//public class ApiService
//{
//    private readonly HttpClient _client = new();
//    public async Task<string> GetApiResponseAsync(string query, string kategoriUsia)
//    {
//        var url = "https://roughly-patient-jolly.ngrok-free.app/ask";

//        var payload = new
//        {
//            query = query,
//            kategori_usia = kategoriUsia
//        };

//        var json = JsonSerializer.Serialize(payload);
//        var 

//        return 
        
//    }
//}
