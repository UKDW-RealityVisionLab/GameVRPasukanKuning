using UnityEngine;

public class TsunamiCheckpoint : MonoBehaviour
{
    public string playerTag = "Player"; // Ensure your Player GameObject is tagged "Player"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("✅ Player reached TsunamiCheckpoint!");

            // Fallback: Try to find GameStateManager if Instance is null
            if (GameStateManager.Instance == null)
            {
                Debug.LogWarning("⚠️ GameStateManager.Instance is null. Attempting to find in scene...");
                GameStateManager foundManager = FindObjectOfType<GameStateManager>();
                if (foundManager != null)
                {
                    GameStateManager.Instance = foundManager;
                    Debug.Log("🔄 GameStateManager.Instance recovered via FindObjectOfType.");
                }
                else
                {
                    Debug.LogError("❌ GameStateManager not found in scene!");
                    return;
                }
            }

            GameStateManager.Instance.OnGameWon();
        }
    }
}
