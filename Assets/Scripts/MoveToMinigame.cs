using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class MoveToMinigame : MonoBehaviour
{
    // [SerializeField] private string addressablePath = "Assets/BundledAsset/minigame/Minigame.unity";
    public AssetReference addressablePath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected. Loading Minigame...");
            LoadMinigameScene();
        }
    }

    private void LoadMinigameScene()
    {
        Addressables.LoadSceneAsync(addressablePath).Completed += OnMinigameLoaded;
    }

    private void OnMinigameLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Minigame loaded successfully.");
        }
        else
        {
            Debug.LogError($"Failed to load Minigame scene from Addressables: {obj.OperationException}");
        }
    }
}
