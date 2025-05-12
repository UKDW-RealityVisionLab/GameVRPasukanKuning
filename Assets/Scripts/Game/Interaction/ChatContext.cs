using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChatContext : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHeader;
    [SerializeField] private TextMeshProUGUI textIsi;
    [SerializeField] private string headerString;
    [SerializeField] private NPCSellingBehaviour sellContext;

    // Update is called once per frame
    void Update()
    {
        GetContext();
    }
    public void GetContext()
    {
        textHeader.text = headerString;
        textIsi.text = "Selamat datang";
    }

    public void SetContext()
    {

    }
}
