using UnityEngine;
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    void Awake() => Instance = this;

    public void OnGameLost()
    {
        // SceneManager.LoadScene("GameOverScene");
    }
    
    // Called when no NPCs drown (timeout)
    public void OnGameWon()
    {
        Debug.Log("You Win: No NPCs drowned!");
        // SceneManager.LoadScene("YouWinScene");
    }
}
