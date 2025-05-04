using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel; // Assign the MainMenuPanel in the inspector
    public GameObject levelSelector;   // Assign the levelSelector in the inspector
    public GameObject tutorialSelector;   // Assign the tutorialSelector in the inspector
    public GameObject movementPanel;   // Assign the movementPanel in the inspector
    public GameObject grabPanel;   // Assign the tutorialSelector in the inspector
    public GameObject teleportPanel;   // Assign the tutorialSelector in the inspector

    public GameObject[] listGambarMovement;
    public GameObject[] listGambarGrab;
    public GameObject[] listGambarTeleport;
    public int index = 0;

    private void Update()
    {
        if(index >= 4)
        {
            index = 4;
        }
        if(index < -1)
        {
            index = -1;
        }
        if (movementPanel.activeSelf)
        {
            if (index == 0)
            {
                listGambarMovement[0].gameObject.SetActive(true);
                listGambarMovement[3].gameObject.SetActive(false);
            }
            if (index < 0 || index >= 4)
            {
                movementPanel.SetActive(false);
                tutorialSelector.SetActive(true);
                index = 0;
            }
        }
        else if (grabPanel.activeSelf)
        {
            if (index == 0)
            {
                listGambarGrab[0].gameObject.SetActive(true);
                listGambarGrab[3].gameObject.SetActive(false);
            }
            if (index < 0 || index >= 4)
            {
                grabPanel.SetActive(false);
                tutorialSelector.SetActive(true);
                index = 0;
            }
        }
        else if (teleportPanel.activeSelf)
        {
            if (index == 0)
            {
                listGambarTeleport[0].gameObject.SetActive(true);
                listGambarTeleport[3].gameObject.SetActive(false);
            }
            if (index < 0 || index >= 4)
            {
                teleportPanel.SetActive(false);
                tutorialSelector.SetActive(true);
                index = 0;
            }
        }
        Debug.Log(index);
    }

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
        // Show the Main Menu Panel and hide the levelSelector Panel
        MainMenuPanel.SetActive(true);
        levelSelector.SetActive(false);

    }
    public void BackTutorial()
    {
        // Show the Main Menu Panel and hide the tutorialSelector Panel
        MainMenuPanel.SetActive(true);
        tutorialSelector.SetActive(false);
    }

    public void Tutorial()
    {
        // Hide the Main Menu Panel and show the tutorialSelector Panel
        MainMenuPanel.SetActive(false);
        tutorialSelector.SetActive(true);
    }
    public void MovementUI()
    {
        tutorialSelector.SetActive(false);
        movementPanel.SetActive(true);
        listGambarMovement[3].gameObject.SetActive(false);
    }
    public void GrabUI()
    {
        tutorialSelector.SetActive(false);
        grabPanel.SetActive(true);
        listGambarGrab[3].gameObject.SetActive(false);
    }
    public void TeleportUI()
    {
        tutorialSelector.SetActive(false);
        teleportPanel.SetActive(true);
        listGambarTeleport[3].gameObject.SetActive(false);
    }
    public void NextTutorialMovement()
    {
        index += 1;
        for(int i = 0; i < listGambarMovement.Length; i++)
        {
            listGambarMovement[i].gameObject.SetActive(false);
            listGambarMovement[index].gameObject.SetActive(true);
        }
    }

    public void PrevTutorialMovement()
    {
        index -= 1;
        for (int i = 0; i < listGambarMovement.Length; i++)
        {
            listGambarMovement[i].gameObject.SetActive(false);
            listGambarMovement[index].gameObject.SetActive(true);
        }
    }

    public void NextTutorialGrab()
    {
        index += 1;
        for (int i = 0; i < listGambarGrab.Length; i++)
        {
            listGambarGrab[i].gameObject.SetActive(false);
            listGambarGrab[index].gameObject.SetActive(true);
        }
    }

    public void PrevTutorialGrab()
    {
        index -= 1;
        for (int i = 0; i < listGambarGrab.Length; i++)
        {
            listGambarGrab[i].gameObject.SetActive(false);
            listGambarGrab[index].gameObject.SetActive(true);
        }
    }

    public void NextTutorialTeleport()
    {
        index += 1;
        for (int i = 0; i < listGambarTeleport.Length; i++)
        {
            listGambarTeleport[i].gameObject.SetActive(false);
            listGambarTeleport[index].gameObject.SetActive(true);
        }
    }

    public void PrevTutorialTeleport()
    {
        index -= 1;
        for (int i = 0; i < listGambarTeleport.Length; i++)
        {
            listGambarTeleport[i].gameObject.SetActive(false);
            listGambarTeleport[index].gameObject.SetActive(true);
        }
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
