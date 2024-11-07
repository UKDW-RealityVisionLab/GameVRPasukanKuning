using UnityEngine;
using UnityEngine.SceneManagement;

public class HandMenuController : MonoBehaviour
{


    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }


}
