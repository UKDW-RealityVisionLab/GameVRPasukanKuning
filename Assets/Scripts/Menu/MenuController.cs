using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel; // Assign the MainMenuPanel in the inspector
    public GameObject levelSelector;   // Assign the levelSelector in the inspector

    // This method loads the main scene or game scene.
    public void StartGame()
    {
        // Hide the Main Menu Panel and show the levelSelector Panel
        MainMenuPanel.SetActive(false);
        levelSelector.SetActive(true);

    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");

    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");

    }

    public void Back()
    {
        // Hide the Main Menu Panel and show the levelSelector Panel
        MainMenuPanel.SetActive(true);
        levelSelector.SetActive(false);

    }

    // This method quits the application.
    public void ExitGame()
    {
        // Quits the application when running as a built executable.
        Application.Quit();
        // The following line only works in the Unity editor to simulate exit.
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
