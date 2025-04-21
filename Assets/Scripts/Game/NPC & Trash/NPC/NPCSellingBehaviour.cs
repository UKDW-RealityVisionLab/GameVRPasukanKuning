using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCSellingBehaviour : MonoBehaviour
{
    public GameObject[] dropItems;             // Items to be dropped (e.g., input by user)
    public Transform trashPoint;               // Tujuan untuk buang sampah
    public Transform sellPoint;                // Tujuan untuk menjual
    public Transform shelfPoint;               // Tujuan untuk menaruh barang di rak

    private int currentTargetIndex = 0;
    private NavMeshAgent agent;

    [SerializeField]
    public Animator animator;

    private Transform[] patrolPoints;
    private bool isPatrolling = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();

        patrolPoints = new Transform[] { trashPoint, sellPoint, shelfPoint };
        GoToNextPoint();
    }

    private void Update()
    {
        if (!isPatrolling) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(HandlePointAction());
        }
    }

    private void GoToNextPoint()
    {
        if (currentTargetIndex >= patrolPoints.Length)
        {
            currentTargetIndex = 0;
        }

        Transform target = patrolPoints[currentTargetIndex];
        agent.SetDestination(target.position);
        animator.SetBool("isWalking", true);
    }

    private IEnumerator HandlePointAction()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);

        Transform currentPoint = patrolPoints[currentTargetIndex];

        if (currentPoint == trashPoint)
        {
            animator.SetTrigger("throw");
            DropItemsAtPoint(trashPoint.position);
        }
        else if (currentPoint == sellPoint)
        {
            animator.SetTrigger("sell");
        }
        else if (currentPoint == shelfPoint)
        {
            animator.SetTrigger("place");
        }

        yield return new WaitForSeconds(1f); // Waktu tunggu untuk animasi (opsional)

        currentTargetIndex++;
        GoToNextPoint();
        agent.isStopped = false;
    }

    private void DropItemsAtPoint(Vector3 position)
    {
        if (dropItems == null || dropItems.Length == 0) return;

        foreach (GameObject item in dropItems)
        {
            if (item != null)
            {
                Instantiate(item, position, Quaternion.identity);
                Debug.Log("Dropped item at trash point: " + position);
            }
        }

        dropItems = new GameObject[0];
    }

    public void StopPatrol()
    {
        isPatrolling = false;
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
    }

    public void StartPatrol()
    {
        isPatrolling = true;
        currentTargetIndex = 0;
        GoToNextPoint();
    }
}
