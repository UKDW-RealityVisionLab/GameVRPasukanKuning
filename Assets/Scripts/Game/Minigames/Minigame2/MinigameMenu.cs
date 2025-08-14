using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class MinigameMenu : MonoBehaviour
{
    public AssetReference retrySceneReference;
    public AssetReference level3SceneReference;

    public void Retry()
    {
        LoadScene(retrySceneReference);

    }
    public void BackToLevel3()
    {
        LoadScene(level3SceneReference);

    }
    
    private void LoadScene(AssetReference sceneReference)
    {
        if (sceneReference.RuntimeKeyIsValid())
        {
            Addressables.LoadSceneAsync(sceneReference).Completed += OnSceneLoaded;
        }
        else
        {
            Debug.LogError($"Invalid scene reference: {sceneReference}");
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


}
