using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPCAIPatrol npcPatrol;  // Reference to the NPCAIPatrol component
    public bool canPatrol = true;  // Flag to control patrolling

    private void Start()
    {
        // Set initial state based on `canPatrol`
        if (canPatrol)
        {
            npcPatrol.StartPatrolAndDrop();
        }
        else
        {
            npcPatrol.StopPatrolAndDrop();
        }
    }

    // Call this method to allow patrolling and item dropping
    public void EnablePatrol()
    {
        canPatrol = true;
        npcPatrol.StartPatrolAndDrop();
    }

    // Call this method to stop patrolling and item dropping
    public void DisablePatrol()
    {
        canPatrol = false;
        npcPatrol.StopPatrolAndDrop();
    }
}
