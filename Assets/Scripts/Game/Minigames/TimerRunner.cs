using UnityEngine;

public class TimerRunner : MonoBehaviour
{
    public TimerData timer;

    private bool gameOverTriggered = false;

    private void Start()
    {
        Debug.Log("🚀 TimerRunner started");

        if (timer != null)
        {
            Debug.Log("✅ TimerData assigned");

            // Optional reset for debug
            timer.currentTime = 0f;
            timer.isRunning = true;
        }
        else
        {
            Debug.LogError("❌ TimerData not assigned in TimerRunner!");
        }
    }
    private void Update()
    {
        if (timer != null && timer.isRunning)
        {
            timer.currentTime += Time.deltaTime;
            Debug.Log($"⏱ Timer running: {timer.currentTime:F2} / {timer.duration}");

            if (timer.currentTime >= timer.duration && !gameOverTriggered)
            {
                Debug.Log("⚠️ Timer expired condition met!");

                gameOverTriggered = true;
                timer.isRunning = false;

                if (!GameStateManager.InstanceWinTriggered)
                {
                    GameStateManager foundManager = FindObjectOfType<GameStateManager>();

                    if (foundManager != null)
                    {
                        Debug.Log("💀 Timer expired. Triggering Game Over...");
                        foundManager.OnGameOver();
                    }
                    else
                    {
                        Debug.LogError("❌ No GameStateManager found in scene!");
                    }
                }
            }
        }
    }

}
