using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameMenu : MonoBehaviour
{


    public void Retry()
    {
        SceneManager.LoadScene("minigame1");

    }
    public void BackToLevel3()
    {
        SceneManager.LoadScene("Level3");

    }


}
