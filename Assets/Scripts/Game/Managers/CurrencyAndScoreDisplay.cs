using UnityEngine;
using TMPro;  // For TextMeshPro
using UnityEngine.SceneManagement; // For scene management

public class CurrencyAndScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI koinText;  // Reference to the Text component that will display the currency
    public TextMeshProUGUI scoreText;  // Reference to the Text component that will display the score

    private void OnEnable()
    {
        // Whenever this object is enabled, update the display
        UpdateKoinText();
        UpdateScoreText();
    }

    private void Update()
    {
        // Update every frame
        UpdateKoinText();
        UpdateScoreText();
    }

    // Method to update the currency display on the UI
    private void UpdateKoinText()
    {
        if (koinText != null)
        {
            koinText.text = "Total Koin: " + GameManager.Koin.ToString();
        }
        else
        {
            Debug.LogError("Koin Text is not assigned in the CurrencyManager.");
        }
    }

    // Method to update the score display
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Total Score: " + GameManager.Score.ToString();  // Display score from GameManager
        }
        else
        {
            Debug.LogError("Score Text is not assigned in the ScoreManager.");
        }
    }
}
