using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TruckAIPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;         // Array of patrol points
    private NavMeshAgent navMeshAgent;      // NavMeshAgent component
    private int currentPatrolIndex;          // Index of the current patrol point
    public TruckController truckController;  // Reference to the TruckController
    public float waitTime = 5f;              // Time to wait at the first patrol point

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        currentPatrolIndex = 0; // Start at the first patrol point
        MoveToNextPatrolPoint(); // Start patrolling
    }

    void Update()
    {
        // Check if the truck has reached the current patrol point
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            // If we are at the first patrol point
            if (currentPatrolIndex == 0)
            {
                // Stop the truck and wait for the external variable
                navMeshAgent.isStopped = true; // Stop the truck
                StartCoroutine(WaitForResume()); // Start waiting for the external variable
            }
            else
            {
                // Move to the next patrol point only if we're not waiting
                if (!navMeshAgent.isStopped)
                {
                    MoveToNextPatrolPoint();
                }
            }
        }
    }

    private IEnumerator WaitForResume()
    {
        // Wait until the canResumePatrol variable is true
        while (!truckController.canResumePatrol)
        {
            yield return null; // Wait until the next frame
        }

        // Once the variable is true, resume patrolling
        navMeshAgent.isStopped = false; // Restart movement
        MoveToNextPatrolPoint(); // Move to the next patrol point
    }

    void MoveToNextPatrolPoint()
    {
        // If there are no patrol points, return
        if (patrolPoints.Length == 0)
            return;

        // Set the next destination of the NavMeshAgent to the current patrol point
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;

        // Update the index to point to the next patrol point
        currentPatrolIndex++;

        // Check if we have reached the end of the patrol points
        if (currentPatrolIndex >= patrolPoints.Length)
        {
            currentPatrolIndex = 0; // Loop back to the first point
        }
    }
}
