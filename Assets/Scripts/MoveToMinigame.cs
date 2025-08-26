using UnityEngine;

public class MoveToMinigame : MonoBehaviour
{
    // [SerializeField] private string addressablePath = "Assets/BundledAsset/minigame/Minigame.unity";
    //public AssetReference addressablePath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected. Loading Minigame...");
            SceneLoader.LoadSceneByName("Minigame2");
        }
    }
}
