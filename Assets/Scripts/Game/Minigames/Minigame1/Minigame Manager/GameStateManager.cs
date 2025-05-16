using UnityEngine;
using System.Collections;
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    void Awake() => Instance = this;

    // Called when no NPCs drown (timeout)
    public void OnGameWon()
    {
        StartCoroutine(DelayedWin());
    }

    private IEnumerator DelayedWin()
    {
        Debug.Log("✅ Win detected. Waiting 30 seconds before confirming...");
        yield return new WaitForSeconds(30f);

        Debug.Log("🎉 You Win: No NPCs drowned!");
        // SceneManager.LoadScene("Level3");
    }
}
