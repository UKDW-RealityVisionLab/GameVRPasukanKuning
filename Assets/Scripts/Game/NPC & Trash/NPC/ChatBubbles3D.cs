using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubbles3D : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Image backgroundImage;       // Replacing backgroundSprite
    [SerializeField] private TextMeshProUGUI headerText;  // Replacing header text
    [SerializeField] private TextMeshProUGUI textMesh;

    [Header("Navigation Buttons")]
    [SerializeField] private Button nextButton;

    [Header("Choice Buttons")]
    [SerializeField] private Button[] choiceButtons;

    public static ChatBubbles3D Create(Transform parent, Vector3 localPosition, string message, string headerText)
    {
        // Instantiate the prefab from GameAssets
        GameObject chatBubblePrefab = GameAssets.Instance.chatBubblePrefab;
        GameObject chatBubbleObject = Instantiate(chatBubblePrefab, parent);

        // Set local position
        chatBubbleObject.transform.localPosition = localPosition;

        // Get ChatBubbles3D component and initialize it
        ChatBubbles3D chatBubble = chatBubbleObject.GetComponent<ChatBubbles3D>();
        chatBubble.Setup(message, headerText);

        // Auto-destroy after 4 seconds
        Destroy(chatBubbleObject, 4f);

        return chatBubble;
    }


    private void Awake()
    {
        // Find references via hierarchy
        backgroundImage = transform.Find("Background")?.GetComponent<Image>();
        headerText = transform.Find("HeaderText")?.GetComponent<TextMeshProUGUI>();
        textMesh = transform.Find("Text")?.GetComponent<TextMeshProUGUI>();

        // Find optional buttons (if needed, ensure names match in hierarchy)
        Transform nextButtonTransform = transform.Find("NextButton");
        if (nextButtonTransform != null)
        {
            nextButton = nextButtonTransform.GetComponent<Button>();
        }

        Transform choicesContainer = transform.Find("Choices");
        if (choicesContainer != null)
        {
            choiceButtons = choicesContainer.GetComponentsInChildren<Button>();
        }
    }

    public void Setup(string message, string header)
    {
        // Set main text and header
        textMesh.text = message;
        headerText.text = header;

        // Adjust background size based on text content
        textMesh.ForceMeshUpdate();
        Vector2 textSize = textMesh.GetRenderedValues(false);
        backgroundImage.rectTransform.sizeDelta = textSize + new Vector2(100f, 50f); // Add padding
    }
}
