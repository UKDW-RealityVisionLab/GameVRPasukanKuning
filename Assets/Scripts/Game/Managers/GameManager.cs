using UnityEngine;

public static class GameManager
{
    public static int Score;
    public static int Koin; // Currency
    public static int TargetKoin = 300; // Target currency for Level 1
    public static int TargetKoin1 = 300; // Target currency for Level 2
    public static int TargetTutorial = 1; // Target currency for Tutorial Level
    public static float TimeRemaining; // Timer for Level 2

    // Initialize game state
    public static void InitGame(string levelIdentifier)
    {
        Score = 0;
        Koin = 0;
        
        // Set the timer only for Level 2
        if (levelIdentifier == "Level2")
        {
            TimeRemaining = 600f; // 10 minutes for Level 2
        }
        else
        {
            TimeRemaining = Mathf.Infinity; // No time limit for Level 1
        }
    }

    // Add score (for example, when player completes a task)
    public static void AddScore(int points)
    {
        Score += points;
    }

    // Add currency (when player collects coins, etc.)
    public static void AddKoin(int amount)
    {
        Koin += amount;
    }

    // Check if the currency goal is reached for Level 1
    public static bool IsCurrencyGoalReached()
    {
        return Koin >= TargetKoin;
    }

    // Check if the currency goal is reached for Level 2
    public static bool IsCurrencyGoalReached1()
    {
        return Koin >= TargetKoin1;
    }

    // Check if the currency goal is reached for Tutorial Level
    public static bool IsCurrencyGoalReached2()
    {
        return Koin >= TargetTutorial;
    }

    // Update timer each frame for Level 2
    public static void UpdateTimer(float deltaTime)
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= deltaTime;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0; // Ensure it doesn’t go below zero
                Debug.Log("Time is up for Level 2!");
                // Handle timer end here, if needed, outside CheckCurrencyGoal
            }
        }
    }
}
