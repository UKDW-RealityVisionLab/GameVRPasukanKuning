public static class GameManager
{
    public static int Score;
    public static int Koin; // Currency
    public static int TargetKoin = 100; // target currency

    // Initialize game state
    public static void InitGame()
    {
        Score = 0;
        Koin = 0;
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

    // Check if the currency goal is reached
    public static bool IsCurrencyGoalReached()
    {
        return Koin >= TargetKoin;
    }
}
