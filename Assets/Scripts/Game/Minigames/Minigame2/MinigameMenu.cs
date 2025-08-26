
using UnityEngine;

public class MinigameMenu : MonoBehaviour
{
    [Header("Target Scenes (nama harus sama seperti di Build Settings)")]
    [SerializeField] private string retrySceneName = "Minigame"; // ganti sesuai kebutuhan
    [SerializeField] private string level3SceneName = "Level3";  // ganti sesuai kebutuhan

    public void Retry()
    {
        SceneLoader.LoadSceneByName(retrySceneName);

    }
    public void BackToLevel3()
    {
        SceneLoader.LoadSceneByName(level3SceneName);
    }
   
}
