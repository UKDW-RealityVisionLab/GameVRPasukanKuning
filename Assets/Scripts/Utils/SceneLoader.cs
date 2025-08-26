using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    /// <summary>Load scene by name (default: Single).</summary>
    public static AsyncOperation LoadSceneByName(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[SceneLoader] sceneName kosong.");
            return null;
        }

        if (Application.CanStreamedLevelBeLoaded(sceneName) == false)
        {
            Debug.LogError($"[SceneLoader] Scene '{sceneName}' belum didaftarkan di Build Settings.");
            return null;
        }

        Debug.Log($"[SceneLoader] Loading scene: {sceneName} ({mode})");
        return SceneManager.LoadSceneAsync(sceneName, mode);
    }

    /// <summary>Reload scene aktif saat ini.</summary>
    public static AsyncOperation ReloadActive()
    {
        var scene = SceneManager.GetActiveScene();
        return LoadSceneByName(scene.name, LoadSceneMode.Single);
    }

    /// <summary>Load additive (menambah scene tanpa menutup yang aktif).</summary>
    public static AsyncOperation LoadAdditive(string sceneName) =>
        LoadSceneByName(sceneName, LoadSceneMode.Additive);

    /// <summary>Unload scene additive.</summary>
    public static AsyncOperation Unload(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            Debug.LogWarning($"[SceneLoader] Scene '{sceneName}' belum loaded.");
            return null;
        }
        Debug.Log($"[SceneLoader] Unloading scene: {sceneName}");
        return SceneManager.UnloadSceneAsync(sceneName);
    }
}
