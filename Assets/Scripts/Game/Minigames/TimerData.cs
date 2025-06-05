using UnityEngine;

[CreateAssetMenu(fileName = "New Timer", menuName = "Game/TimerData")]
public class TimerData : ScriptableObject
{
    public float duration = 10f;

    [HideInInspector] public float currentTime;
    [HideInInspector] public bool isRunning;

    public void StartTimer()
    {
        currentTime = 0f;
        isRunning = true;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        isRunning = false;
    }

    public bool IsFinished => currentTime >= duration;
}
