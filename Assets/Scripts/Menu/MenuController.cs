using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject levelSelector;

    [SerializeField] Slider downloadProgressSlider; // Slider
    [SerializeField] GameObject downloadProgressPanel; // Parent panel
    [SerializeField] TextMeshProUGUI downloadProgressText; // To display percentage

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
        StartCoroutine(LoadSceneWithProgress("Assets/BundledAsset/level1/Level 1.unity"));
    }

    public void Level2()
    {
        StartCoroutine(LoadSceneWithProgress("Assets/BundledAsset/level2/Level 2.unity"));
    }

    public void Level3()
    {
        StartCoroutine(LoadSceneWithProgress("Assets/BundledAsset/level3/Level 3.unity"));
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

    //private void LoadSceneUsingAddressables(string sceneKey)
    //{
    //    Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single).Completed += OnSceneLoadComplete;
    //}

    //private void OnSceneLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    //{
    //    if (handle.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        Debug.Log("Scene loaded successfully: " + handle.Result.Scene.name);
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to load scene using Addressables: " + handle.DebugName);
    //    }
    //}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Debug.Log("Unloading unused Addressables...");
            Addressables.ReleaseInstance(gameObject);
        }
    }

    private IEnumerator LoadSceneWithProgress(string sceneKey)
    {
        // Show loading panel
        downloadProgressPanel.SetActive(true);
        downloadProgressSlider.value = 0f;

        // Show "Downloading..." message
        if (downloadProgressText)
        {
            downloadProgressText.gameObject.SetActive(true);
            downloadProgressText.text = "Downloading...";
        }

        // Start loading scene asynchronously
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single);

        // Wait for progress
        while (!handle.IsDone)
        {
            float progress = handle.PercentComplete;
            downloadProgressSlider.value = progress;

            // Show percentage during download
            if (downloadProgressText)
                downloadProgressText.text = $"Downloading... {Mathf.RoundToInt(progress * 100f)}%";

            yield return null;
        }

        // Handle result
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene loaded successfully: " + handle.Result.Scene.name);
        }
        else
        {
            Debug.LogError("Failed to load scene using Addressables: " + handle.DebugName);

            // Show error message
            if (downloadProgressText)
            {
                downloadProgressText.gameObject.SetActive(true);
                downloadProgressText.text = "Download Error!";
            }

            // Optional: hide slider
            downloadProgressSlider.value = 0f;
        }
    }


}
