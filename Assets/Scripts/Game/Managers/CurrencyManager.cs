using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // For scene management

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI koinText;  // Reference to the Text component that will display the currency

    private void Start()
    {
        // Ensure the GameManager is initialized
        GameManager.InitGame();

        if (koinText == null)
        {
            // Try to find the UI Text component if it wasn't assigned in the inspector
            koinText = FindObjectOfType<TextMeshProUGUI>();
            if (koinText != null)
            {
                Debug.Log("Currency Text found.");
            }
            else
            {
                Debug.LogError("Currency UI Text not found in the scene!");
            }
        }
        else
        {

        }
    }

    // Method to update the currency
    public void UpdateKoin(int koinChange)
    {
        GameManager.AddKoin(koinChange);  // Add to the static Koin in GameManager
        Debug.Log($"Total Koin: {GameManager.Koin}");



    }



}
