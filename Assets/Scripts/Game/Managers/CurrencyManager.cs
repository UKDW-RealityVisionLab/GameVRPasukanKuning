using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // For scene management

public class CurrencyManager : MonoBehaviour
{
    // public TextMeshProUGUI koinText;  // Reference to the Text component that will display the currency


    // Method to update the currency
    public void UpdateKoin(int koinChange)
    {
        GameManager.AddKoin(koinChange);  // Add to the static Koin in GameManager
        Debug.Log($"Total Koin: {GameManager.Koin}");



    }



}
