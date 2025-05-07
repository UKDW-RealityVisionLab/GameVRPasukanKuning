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

    [SerializeField] public Slider downloadProgressSlider; // Slider
    [SerializeField] public GameObject downloadProgressPanel; // Parent panel
    [SerializeField] public TextMeshProUGUI downloadProgressText; // To display percentage

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
        ShowDownloadUI();

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single);

        while (!handle.IsDone)
        {
            UpdateDownloadUI(handle.PercentComplete);
            yield return null;
        }

        HandleSceneLoadResult(handle);
    }


    public void ShowDownloadUI()
    {
        if (downloadProgressPanel != null)
        {
            downloadProgressPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ShowDownloadUI: downloadProgressPanel is null.");
        }

        if (downloadProgressSlider != null)
        {
            downloadProgressSlider.value = 0f;
        }
        else
        {
            Debug.LogWarning("ShowDownloadUI: downloadProgressSlider is null.");
        }

        if (downloadProgressText)
        {
            downloadProgressText.gameObject.SetActive(true);
            downloadProgressText.text = "Downloading...";
        }
        else
        {
            Debug.LogWarning("ShowDownloadUI: downloadProgressText is null.");
        }
    }

    public void UpdateDownloadUI(float progress)
    {
        if (downloadProgressSlider != null)
        {
            downloadProgressSlider.value = progress;
        }
        else
        {
            Debug.LogWarning("UpdateDownloadUI: downloadProgressSlider is null.");
        }

        if (downloadProgressText != null)
        {
            downloadProgressText.text = $"Downloading... {Mathf.RoundToInt(progress * 100f)}%";
        }
        else
        {
            Debug.LogWarning("UpdateDownloadUI: downloadProgressText is null.");
        }
    }

    public void ShowDownloadError()
    {
        if (downloadProgressText != null)
        {
            downloadProgressText.gameObject.SetActive(true);
            downloadProgressText.text = "Download Error!";
        }
        else
        {
            Debug.LogWarning("ShowDownloadError: downloadProgressText is null.");
        }

        if (downloadProgressSlider != null)
        {
            downloadProgressSlider.value = 0f;
        }
        else
        {
            Debug.LogWarning("ShowDownloadError: downloadProgressSlider is null.");
        }
    }

    public void HandleSceneLoadResult(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene loaded successfully: " + handle.Result.Scene.name);
        }
        else
        {
            Debug.LogError("Failed to load scene using Addressables: " + handle.DebugName);
            ShowDownloadError();
        }
    }



    public IEnumerator TestableLoadScene(string sceneKey)
    {
        return LoadSceneWithProgress(sceneKey);
    }

}
