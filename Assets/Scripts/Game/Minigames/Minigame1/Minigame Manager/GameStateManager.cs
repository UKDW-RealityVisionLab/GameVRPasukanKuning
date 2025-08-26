using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;


    [Header("Scene Names (Build Settings)")]
    [SerializeField] private string winSceneName = "Level 3";
    [SerializeField] private string gameOverSceneName = "Fail";

    [Header("Game Settings")]
    [SerializeField] private float winDelay = 30f;

    private Coroutine winCoroutine;

    public static bool InstanceWinTriggered = false;

    [Header("Achivement Settings")]
    public ChecklistDatabase checklistDatabase;
    public MinigameChecklistItem minigameChecklistItem;

    private void Awake()
    {
        // optional singleton
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        // Uncomment kalau ingin persist di antara scene:
        // DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Call this when game is won (e.g., all NPCs safe).
    /// </summary>
    public void OnGameWon()
    {
        if (winCoroutine != null)
            StopCoroutine(winCoroutine);

        InstanceWinTriggered = true;

        winCoroutine = StartCoroutine(DelayedWin());
    }


    public void OnGameOver()
    {
        if (string.IsNullOrEmpty(gameOverSceneName))
        {
            Debug.LogWarning("⚠️ Game Over scene name kosong. Skip load Game Over.");
            return;
        }

        if (winCoroutine != null) StopCoroutine(winCoroutine);

        winCoroutine = StartCoroutine(DelayedLose());
    }


    private IEnumerator DelayedWin()
    {
        Debug.Log("✅ Win detected. Waiting " + winDelay + " seconds before confirming...");
        yield return new WaitForSeconds(winDelay);

        if (minigameChecklistItem != null)
        {
            minigameChecklistItem.isComplete = true;
            Debug.Log($"🏆 Marked '{minigameChecklistItem.displayName}' as complete!");
        }
        else
        {
            Debug.LogWarning("❗ No MinigameChecklistItem assigned. Cannot mark as complete.");
        }

        // Debug.Log("🎉 You Win: No NPCs drowned!");
        SceneLoader.LoadSceneByName(winSceneName);
    }

    private IEnumerator DelayedLose()
    {
        Debug.Log("✅ Lose detected. Waiting " + winDelay + " seconds before confirming...");
        yield return new WaitForSeconds(winDelay);
        SceneLoader.LoadSceneByName(gameOverSceneName);
    }


#if UNITY_EDITOR
    [ContextMenu("Trigger Win (Debug)")]
    private void DebugTriggerWin()
    {
        OnGameWon();
    }
#endif
}
