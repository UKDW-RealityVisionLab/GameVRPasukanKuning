using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject levelSelector;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        levelSelector.SetActive(true);
    }

    public void Level1()
    {
        LoadSceneUsingAddressables("Assets/BundledAsset/level1/Level 1.unity");
    }

    public void Level2()
    {
        LoadSceneUsingAddressables("Assets/BundledAsset/level2/Level 2.unity");
    }

    public void Back()
    {
        MainMenuPanel.SetActive(true);
        levelSelector.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void LoadSceneUsingAddressables(string sceneKey)
    {
        Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single).Completed += OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene loaded successfully: " + handle.Result.Scene.name);
        }
        else
        {
            Debug.LogError("Failed to load scene using Addressables: " + handle.DebugName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Debug.Log("Unloading unused Addressables...");
            Addressables.ReleaseInstance(gameObject);
        }
    }
}
