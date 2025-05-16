using UnityEngine;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    public NPCFSMController[] npcs;
    public TMP_Text winLoseText;
    public GameObject MenuLose;

    public SocketController[] socketControllers;

    private int inactiveRipCount = 0;
    private int totalHandleRipCurrentBecameActive = 0;
    private bool gameOver = false;

    void OnEnable()
    {

        SocketController.OnRipCurrentBecameActive += HandleRipCurrentBecameActive;
        SocketController.OnRipCurrentBecameInactive += HandleRipCurrentBecameInactive;
    }

    void OnDisable()
    {

        SocketController.OnRipCurrentBecameActive -= HandleRipCurrentBecameActive;
        SocketController.OnRipCurrentBecameInactive -= HandleRipCurrentBecameInactive;
    }

    private void Start() {
        LogRipCurrentStatus();
    }

    void HandleRipCurrentBecameActive(SocketController socket)
    {
        if (gameOver) return;
        totalHandleRipCurrentBecameActive++;
        Debug.Log("Rip Flagged. Total: " + totalHandleRipCurrentBecameActive);
        CheckWin();
    }

    void HandleRipCurrentBecameInactive(SocketController socket)
    {
        if (gameOver) return;
        Debug.Log($"❌ Lose: Rip current on {socket.name} is still active.");
        gameOver = true;
        if (winLoseText != null)
            winLoseText.text = "You Lose";
        MenuLose.SetActive(true);
    }

    void LogRipCurrentStatus()
    {
        int totalRipCurrents = 0;
        int activeRipCurrents = 0;

        foreach (var socket in socketControllers)
        {
            if (socket.ripCurrentObject != null)
            {
                totalRipCurrents++;
                if (!socket.ripCurrentObject.activeSelf)
                    activeRipCurrents++;
            }
        }
        inactiveRipCount = totalRipCurrents - activeRipCurrents;

        Debug.Log($"RipCurrentController Status: Total Rip Currents = {totalRipCurrents}, Active Rip Currents = {activeRipCurrents}");
    }
    
    void CheckWin()
    {
        if (inactiveRipCount == totalHandleRipCurrentBecameActive)
        {
            Debug.Log("✅ Win.");
            gameOver = true;
            if (winLoseText != null)
                winLoseText.text = "You Win";

            GameStateManager.Instance.OnGameWon();
        }
    }
}
