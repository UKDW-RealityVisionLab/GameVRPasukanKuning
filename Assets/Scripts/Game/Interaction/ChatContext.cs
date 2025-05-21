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
        // Pastikan komponen AIBehaviour tersedia
        ai = GetComponent<AIBehaviour>();
        if (ai == null)
        {
            Debug.LogError("AIBehaviour tidak ditemukan di GameObject ini.");
            return;
        }

        // Buat instance listCon dan isi sesuai role AI
        listCon = new ListContext();

        // Debug (opsional)
        Debug.Log("ListContext berhasil di-inisialisasi dengan role: " + ai.Type);
    }

    // Update is called once per frame
    void Update()
    {
        if (listCon == null || ai == null) return;

        GetContextQuestion();

        chatTimer += Time.deltaTime;

        if (chatTimer >= chatInterval)
        {
            //ShowRandomDailyChat();
            chatTimer = 0f;
            GetRandomChat();
        }
    }

    public void GetContextQuestion()
    {
        textHeader.text = headerString;
        var questions = listCon.GetQuestion(ai);
        if (questions != null && questions.Length > 0)
        {
            textIsi.text = questions[Random.Range(0,questions.Length)];
        }
    }
    public void GetRandomChat()
    {
        var chats = listCon.GetRandomChat(ai);
        if (chats != null && chats.Length > 0)
        {
            ShowNPCDialog(chats[Random.Range(0, chats.Length)]);
        }
    }

    public void NextButton()
    {
        textIsi.text = "";
    }
    public void ShowNPCDialog(string message, float duration = 3f)
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
