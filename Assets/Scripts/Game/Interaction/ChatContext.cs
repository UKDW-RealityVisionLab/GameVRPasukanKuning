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
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEditor.Search;

public class ChatContext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHeader;
    [SerializeField] public TextMeshProUGUI textIsi;
    [SerializeField] private string headerString;
    //[SerializeField] private InputActionReference rightNpc;
    [SerializeField] private string query;
    [SerializeField] private string age;
    public GameObject npcDialogUI;
    public TextMeshProUGUI dialogRandomText;
    private AIBehaviour ai;
    private NPCInteractable npcInteract;
    [SerializeField] private float chatInterval = 10f; // tiap berapa detik muncul dialog
    private float chatTimer = 0f;
    private ListContext listCon;
    private string currentGuideDestination = "";
    private int currentIndex = 0;
    private string[] currentTexts = null;


    private JsonData dialogData;


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


        chatTimer += Time.deltaTime;

        if (chatTimer >= chatInterval && ai.isInteracting == false)
        {
            chatTimer = 0f;
            GetRandomChat();
        }
        //if (rightNpc.action.WasPressedThisFrame())
        //{
        //    UpdateTextContent();
        //}
    }
    //private void UpdateTextContent()
    //{
    //    if (currentTexts != null && currentTexts.Length > 0)
    //    {
    //        textIsi.text = currentTexts[currentIndex];
    //    }
    //}

    public void GetContextQuestion()
    {
        textHeader.text = headerString;
        var questions = listCon.GetQuestion(ai);
        if (questions != null && questions.Length > 0)
        {
            currentTexts = questions;
            currentIndex = 0;
            textIsi.text = currentTexts[Random.Range(currentIndex, currentTexts.Length)];
        }
    }
    public void GetAnswer(string context)
    {
        textHeader.text = headerString;
        var answers = listCon.GetAnswer(ai,context);
        if (answers != null && answers.Length > 0)
        {
            textIsi.text = answers[Random.Range(0, answers.Length)];
        }
    }

    public void GetIntroduction()
    {
        textHeader.text = headerString;
        var introduction = listCon.GetIntroduction(ai);
        if (introduction != null && introduction.Length > 0)
        {
            textIsi.text = introduction[Random.Range(0, introduction.Length)];
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
    public void GetAngryContext()
    {
        textHeader.text = headerString;
        var angry = listCon.GetAngryChat(ai);
        if (angry != null && angry.Length > 0)
        {
            textIsi.text = angry[Random.Range(0, angry.Length)];
        }
    }

    public void GetAfterAngryChat()
    {
        textHeader.text = headerString;
        var afterAngry = listCon.GetAfterAngryChat(ai);
        if (afterAngry != null && afterAngry.Length > 0)
        {
            textIsi.text = afterAngry[Random.Range(0, afterAngry.Length)];
        }
    }
    public void GetEmotionChat()
    {
        textHeader.text = headerString;
        var emotion = listCon.GetEmotion(ai);
        if (emotion != null && emotion.Length > 0)
        {
            textIsi.text = emotion[Random.Range(0, emotion.Length)];
        }
    }

    public void GetGuideContext()
    {
        textHeader.text = headerString;

        string[] guideTexts = listCon.GetExplanation(ai, currentGuideDestination);
        if (guideTexts != null && guideTexts.Length > 0)
        {
            currentTexts = guideTexts;
            currentIndex = 0;
            textIsi.text = currentTexts[currentIndex];
        }
    }

    public void GetOllamaResponse(string queryBaru)
    {
        query = queryBaru;
        textIsi.text = "Biar saya pikir dahulu";
        StartCoroutine(ApiClient.PostQuery(query, age, (response) =>
        {
            if (!string.IsNullOrEmpty(response))
            {
                Debug.Log("API Response: " + response);
                textIsi.text = response;
            }
            else
            {
                Debug.LogError("API Error: no response.");
            }
        }));
    }
    //public void GetGuideContextInOrder()
    //{
    //    textHeader.text = headerString;

    //    string[] guideTexts = listCon.GetExplanation(ai, currentGuideDestination);
    //    if (guideTexts != null && guideTexts.Length > 0)
    //    {
    //        // Ambil indeks saat ini (default 0 jika belum pernah dipanggil)
    //        if (!guideIndexMap.ContainsKey(currentGuideDestination))
    //        {
    //            guideIndexMap[currentGuideDestination] = 0;
    //        }

    //        int index = guideIndexMap[currentGuideDestination];
    //        textIsi.text = guideTexts[index];

    //        // Update index ke berikutnya (dengan looping kembali ke awal)
    //        guideIndexMap[currentGuideDestination] = (index + 1) % guideTexts.Length;
    //    }
    //}


    public string SetCurrentGuideDestination(string destination)
    {
        currentGuideDestination = destination;
        return currentGuideDestination;
    }

    public void NextButton()
    {
        if (currentTexts == null || currentTexts.Length == 0)
            return;

        currentIndex = (currentIndex + 1) % currentTexts.Length;
        textIsi.text = currentTexts[currentIndex];
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
}
