using UnityEngine;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [Header("Scene References")]
    public AssetReference WinSceneReference;

    [Header("Game Settings")]
    [SerializeField] private float winDelay = 30f;

    private Coroutine winCoroutine;

    /// <summary>
    /// Call this when game is won (e.g., all NPCs safe).
    /// </summary>
    public void OnGameWon()
    {
        if (winCoroutine != null)
            StopCoroutine(winCoroutine);

        winCoroutine = StartCoroutine(DelayedWin());
    }

    private IEnumerator DelayedWin()
    {
        Debug.Log("✅ Win detected. Waiting " + winDelay + " seconds before confirming...");
        yield return new WaitForSeconds(winDelay);

        Debug.Log("🎉 You Win: No NPCs drowned!");
        LoadScene(WinSceneReference);
    }

    private void LoadScene(AssetReference sceneReference)
    {
        if (sceneReference.RuntimeKeyIsValid())
        {
            Addressables.LoadSceneAsync(sceneReference).Completed += OnSceneLoaded;
        }
        else
        {
            Debug.LogError($"❌ Invalid scene reference: {sceneReference}");
        }
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"✅ Scene loaded: {handle.Result.Scene.name}");
        }
        else
        {
            Debug.LogError($"❌ Failed to load scene: {handle.OperationException}");
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Trigger Win (Debug)")]
    private void DebugTriggerWin()
    {
        OnGameWon();
    }
#endif
}
