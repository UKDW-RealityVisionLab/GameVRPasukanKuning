using UnityEngine;
using TMPro;  // Add for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    // public TextMeshProUGUI scoreText;  // Reference to the Text component that will display the score


    // private void Start()
    // {
    //     if (scoreText == null)
    //     {
    //         // Try to find the UI Text component if it wasn't assigned
    //         scoreText = FindObjectOfType<TextMeshProUGUI>();
    //         if (scoreText != null)
    //         {
    //             Debug.Log("Score Text found.");
    //         }
    //         else
    //         {
    //             Debug.LogError("Score UI Text not found in the scene!");
    //         }
    //     }
    // }

    // Method to update the score
    public void UpdateScore(int scoreChange)
    {
        GameManager.AddScore(scoreChange);  // Update score via GameManager
        Debug.Log($"Total Score: {GameManager.Score}");


    }


}
