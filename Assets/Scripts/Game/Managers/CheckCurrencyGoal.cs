using UnityEngine;
using UnityEngine.SceneManagement; // Include this for scene management

public class CheckCurrencyGoal : MonoBehaviour
{
    public string nextSceneName; // Add a reference for the scene name to be loaded
    private bool hasGoalBeenReached = false; // Flag to track if the goal has been reached

    private void Update()
    {
        // If the goal hasn't been reached yet, check if the goal is reached
        if (!hasGoalBeenReached && GameManager.IsCurrencyGoalReached())
        {
            Debug.Log("Currency goal has been reached!");

            // Load the scene once the currency goal is reached
            SceneManager.LoadScene(nextSceneName); // Ensure you assign 'nextSceneName' in the Inspector or dynamically

            // Set the flag to true so we stop checking after this
            hasGoalBeenReached = true;
        }
    }
}
