using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCAIPatrol : MonoBehaviour
{
    public float patrolRadius = 10f;       // Radius within which the character will pick random points
    public GameObject[] itemPrefabs;       // Array of item prefabs to drop
    public Transform dropTarget;           // Drop location or a spawn point (optional)
    public float minStopDuration = 1f;     // Minimum stop duration in seconds
    public float maxStopDuration = 3f;     // Maximum stop duration in seconds
    public float waitAfterDrop = 1f;       // Wait time after dropping an item before resuming patrol

    private NavMeshAgent agent;

    [SerializeField]
    public Animator animator;
    private bool isDroppingItem = false;
    private bool isPatrolling = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>(); // Assign Animator if not already assigned
        GoToRandomPoint();
    }

    private void Update()
    {
        if (!isPatrolling)
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false); // Set to Idle when not patrolling
            return;
        }

        // If the agent reaches its destination, initiate the stop and drop item coroutine
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !isDroppingItem)
        {
            StartCoroutine(StopAndDropItem());
        }
    }

    private void GoToRandomPoint()
    {
        if (!isPatrolling) return;

        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        animator.SetBool("isWalking", true); // Set to Walk
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private IEnumerator StopAndDropItem()
    {
        isDroppingItem = true;
        agent.isStopped = true;
        animator.SetBool("isWalking", false); // Set to Idle while stopping

        float stopDuration = Random.Range(minStopDuration, maxStopDuration);

        if (itemPrefabs.Length > 0 && isPatrolling)
        {
            GameObject itemToDrop = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Vector3 dropPosition = dropTarget ? dropTarget.position : transform.position;
            Instantiate(itemToDrop, dropPosition, Quaternion.identity);
            Debug.Log("Dropped item at: " + dropPosition);
        }

        yield return new WaitForSeconds(stopDuration);

        agent.isStopped = false;
        yield return new WaitForSeconds(waitAfterDrop);

        GoToRandomPoint();
        isDroppingItem = false;
    }

    public void StopPatrolAndDrop()
    {
        isPatrolling = false;
        agent.isStopped = true;
        animator.SetBool("isWalking", false); // Ensure Idle animation
    }

    public void StartPatrolAndDrop()
    {
        isPatrolling = true;
        agent.isStopped = false;
        GoToRandomPoint();
    }
}
