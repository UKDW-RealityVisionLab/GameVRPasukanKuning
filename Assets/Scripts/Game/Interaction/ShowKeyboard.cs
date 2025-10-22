using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Unity.VisualScripting;

public class ShowKeyboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private ChatContext chatCon;
    [SerializeField] private AIBehaviour ai;

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
                //chatCon.GetOllamaResponse(text);
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
        SetCaretColorAlpha(1);

        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    private void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    public void SetCaretColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = value;
        inputField.caretColor = caretColor;
    }
}
