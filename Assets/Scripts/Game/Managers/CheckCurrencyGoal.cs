using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCurrencyGoal : MonoBehaviour
{
    public string levelIdentifier; // Identifier for the current level, e.g., "Level1" or "Level2"
    public string nextSceneName;   // Scene to load when the goal is reached
    public string failSceneName;  // Scene to load if the timer runs out (for Level 2)
    private bool hasGoalBeenReached = false; // Flag to track if the goal has been reached


    private void Start() 
    {
        // Ensure the GameManager is initialized
        GameManager.InitGame(levelIdentifier);
    }

    private void Update()
    {
        if (levelIdentifier == "Level2")
        {
            UpdateLevel2Timer();
        }

        // If the goal hasn't been reached yet, check if the goal is reached
        if (!hasGoalBeenReached)
        {
            if (levelIdentifier == "Level1" && GameManager.IsCurrencyGoalReached())
            {
                OnCurrencyGoalReached();
            }
            else if (levelIdentifier == "Level2" && GameManager.IsCurrencyGoalReached1())
            {
                OnCurrencyGoalReached();
            }
            else if (levelIdentifier == "Tutorial" && GameManager.IsCurrencyGoalReached2())
            {
                OnCurrencyGoalReached();
            }
        }
    }

    private void UpdateLevel2Timer()
    {
        if (GameManager.TimeRemaining > 0)
        {
            GameManager.UpdateTimer(Time.deltaTime); // Decrease the timer
            // Debug.Log("Time Remaining : " + GameManager.TimeRemaining);
        }
        else if (!hasGoalBeenReached)
        {
            Debug.Log("Time's up! Level failed.");
            hasGoalBeenReached = true; // Prevent further checks
            SceneManager.LoadScene(failSceneName); // Load the fail scene
        }
    }

    private void OnCurrencyGoalReached()
    {
        Debug.Log("Currency goal has been reached!");

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);

        // Set the flag to true so we stop checking after this
        hasGoalBeenReached = true;
    }
}
