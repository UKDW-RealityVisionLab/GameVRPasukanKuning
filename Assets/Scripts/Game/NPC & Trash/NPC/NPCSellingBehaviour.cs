using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSellingBehaviour : MonoBehaviour
{
    public GameObject[] dropItems;             // Items to be dropped (e.g., input by user)
    public Transform[] trashPoint;               // Tujuan untuk buang sampah
    public Transform sellPoint;                // Tujuan untuk menjual
    public Transform shelfPoint;               // Tujuan untuk menaruh barang di rak

    private bool goToSellAfterCurrentTask = false;

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

        patrolPoints = CombinePatrolPoints(trashPoint, sellPoint, shelfPoint);
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

    public void SetGoToSellNext()
    {
        goToSellAfterCurrentTask = true;
    }


    private Transform[] CombinePatrolPoints(Transform[] trashPoints, params Transform[] otherPoints)
    {
        List<Transform> combined = new List<Transform>();
        combined.AddRange(trashPoints);
        combined.AddRange(otherPoints);
        return combined.ToArray();
    }

    private void GoToNextPoint()
    {
        if (goToSellAfterCurrentTask)
        {
            goToSellAfterCurrentTask = false; // reset it after going
            agent.SetDestination(sellPoint.position);
            animator.SetBool("isWalking", true);
            return;
        }

        if (currentTargetIndex >= patrolPoints.Length)
        {
            currentTargetIndex = 0;
        }

        Transform target = patrolPoints[currentTargetIndex];
        agent.SetDestination(target.position);
        animator.SetBool("isWalking", true);
    }

    private bool IsTrashPoint(Transform point)
    {
        foreach (Transform trash in trashPoint)
        {
            if (point == trash)
            {
                return true;
            }
        }
        return false;
    }


    private IEnumerator HandlePointAction()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);

        Transform currentPoint = patrolPoints[currentTargetIndex];

        if (IsTrashPoint(currentPoint))
        {
            animator.SetTrigger("throw");
            DropItemsAtPoint(currentPoint.position);
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
