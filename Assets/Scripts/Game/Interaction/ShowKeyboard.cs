using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Unity.VisualScripting;
using UnityEditor.Search;

public class ShowKeyboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private ChatContext chatCon;

    public float distance = 0.5f;
    public float verticalOffset = -0.5f;
    public Transform positionSource;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
        // submit text ke ChatContext saat player tekan Enter/selesai mengetik
        inputField.onEndEdit.AddListener(text =>
        {
        if (chatCon != null && !string.IsNullOrWhiteSpace(text))
        {
                chatCon.GetOllamaResponse(text);
            }
        });
    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetposition = positionSource.position + direction * distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(targetposition);
    }
}
