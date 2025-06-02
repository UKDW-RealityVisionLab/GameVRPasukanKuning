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

    public void Level3()
    {
        StartCoroutine(LoadSceneWithProgress("Assets/BundledAsset/level3/Level 3.unity"));
    }

    public void Tutorial()
    {
        StartCoroutine(LoadSceneWithProgress("Assets/BundledAsset/tutorial/tutorial.unity"));
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

        if (downloadProgressText != null)
        {
            // Here is the new logic:
            if (downloadProgressPanel != null)
            {
                downloadProgressText.gameObject.SetActive(true);
                downloadProgressText.text = downloadProgressSlider != null ? "Downloading..." : "";
            }
            else
            {
                downloadProgressText.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("ShowDownloadUI: downloadProgressText is null.");
        }
    }


    public void UpdateDownloadUI(float progress)
    {
        if (downloadProgressSlider != null && downloadProgressText != null)
        {
            downloadProgressSlider.value = progress;
            downloadProgressText.text = $"Downloading... {Mathf.RoundToInt(progress * 100f)}%";
        }
        if (downloadProgressText == null)
        {
            Debug.LogWarning("UpdateDownloadUI: downloadProgressText is null.");
            if (downloadProgressSlider)
            {
                downloadProgressSlider.value = 0;
            }
        }
        if (downloadProgressSlider == null)
        {
            Debug.LogWarning("UpdateDownloadUI: downloadProgressSlider is null.");
            if (downloadProgressText)
            {
                downloadProgressText.text = "";
            }
        }
        if (downloadProgressText == null && downloadProgressSlider == null)
        {
            Debug.LogWarning("UpdateDownloadUI: downloadProgressSlider is null.");
            Debug.LogWarning("UpdateDownloadUI: downloadProgressText is null.");
        }
    }

    public void ShowDownloadError()
    {
        if (downloadProgressText != null && downloadProgressSlider != null)
        {
            downloadProgressText.gameObject.SetActive(true);
            downloadProgressText.text = "Download Error!";
            downloadProgressSlider.value = 0f;
        }
        if (downloadProgressSlider == null)
        {
            Debug.LogWarning("ShowDownloadError: downloadProgressSlider is null.");
            if (downloadProgressText != null)
            {
                downloadProgressText.gameObject.SetActive(false);
            }
        }

        if (downloadProgressText == null)
        {
            Debug.LogWarning("ShowDownloadError: downloadProgressText is null.");
            if (downloadProgressSlider != null)
            {
                downloadProgressSlider.value = 0f;
            }
        }
       
        if (downloadProgressSlider == null && downloadProgressText == null)
        {
            Debug.LogWarning("ShowDownloadError: downloadProgressSlider is null.");
            Debug.LogWarning("ShowDownloadError: downloadProgressText is null.");
        }
    }

    public void HandleSceneLoadResult(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene loaded successfully: " + handle.Result.Scene.name);
            if (downloadProgressText != null)
            {
                downloadProgressText.text = ""; // Explicitly set to empty if it's a success.
            }
            if (downloadProgressSlider != null)
            {
                downloadProgressSlider.value = 1.0f;
            }

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
