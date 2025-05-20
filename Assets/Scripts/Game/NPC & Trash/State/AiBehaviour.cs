using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    public NPCType Type;
    public Animator animator;
    public NavMeshAgent agent;
    private float timer;
    private float distance = 2f;
    public float actionInterval = 10f; // waktu antar aktivitas acak
    public List<System.Action> availableActions = new List<System.Action>();
    public bool isInteracting = false;
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private Transform dropPos;
    public float patrolRadius = 15f;
    public bool isPatrolling = true;
    public float detectRadius = 7f; // Jarak untuk mendeteksi NPC lain
    public string npcTag = "NPC";   // Tag yang digunakan untuk NPC
    public bool hasJustLittered = false;
    public bool isChasing = false;
    private Transform chaseTarget; // target yang sedang dikejar


    public Transform targetNataBarang;
    public Transform targetSampah;
    public Transform targetCustomer;
    public Vector3 randomPos;

    public StateMachine stateMachine;
    public IdleState idleState;
    public WanderingState wanderingState;
    public ActivityState activityState;
    public GuidanceState guidanceState;
    private AIActivitySelector activitySelector;
    private NPCInteractable npcInter;

    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new IdleState(stateMachine, this);
        wanderingState = new WanderingState(stateMachine, this);
        activityState = new ActivityState(stateMachine, this);
        guidanceState = new GuidanceState(stateMachine, this);
        activitySelector = new AIActivitySelector(this);
        randomPos = GetRandomNavmeshPosition();
        //StartCoroutine(RunRoutineByRole());
        stateMachine.ChangeState(idleState);
        if (Type == NPCType.BystanderSecurity)
        {
            StartCoroutine(CheckForLitteringNPC());
        }
    }

    void Update()
    {
        if (isInteracting == false) // Hanya lakukan aktivitas kalau tidak interaksi
        {
            timer += Time.deltaTime;
            if (timer >= actionInterval)
            {
                timer = 0f;
                ResetIdleBools();
                activitySelector.ChooseRandomActivity();
            }
        }
        else
        {
            return;
        }

            stateMachine.Update();
    }

    public void EnterTalkingState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new TalkingState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }

    public void DropItem()
    {
        if (itemPrefabs == null || itemPrefabs.Length == 0 || dropPos == null)
        {
            Debug.LogWarning("Item prefabs atau dropPos belum diset.");
            return;
        }

        int index = Random.Range(0, itemPrefabs.Length);
        GameObject itemToDrop = itemPrefabs[index];

        GameObject droppedItem =  Instantiate(itemToDrop, dropPos.position, Quaternion.identity);

        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 throwDirection = (transform.forward + Vector3.up).normalized;
            float throwForce = 3f;

            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        hasJustLittered = true;
        StartCoroutine(ResetLitterFlag());
    }
    private IEnumerator ResetLitterFlag()
    {
        yield return new WaitForSeconds(5f); // durasi dianggap 'baru saja' buang sampah
        hasJustLittered = false;
    }

    public Transform GetDetectedTrashThrower()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
        foreach (var npc in npcs)
        {
            if (npc == this.gameObject) continue;

            AIBehaviour other = npc.GetComponent<AIBehaviour>();
            if (other != null && other.hasJustLittered == true)
            {
                return npc.transform;
            }
        }
        return null;
    }


    public Transform GetRandomChairPosition()
    {
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Chair");
        if (chairs.Length == 0) return null;

        int index = Random.Range(0, chairs.Length);
        return chairs[index].transform;
    }
    public Vector3 GetRandomNavmeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position; // fallback kalau gagal
    }

    IEnumerator CheckForLitteringNPC()
    {
        while (true)
        {
            if (!isChasing)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius);
                foreach (var hit in hits)
                {
                    AIBehaviour otherAI = hit.GetComponent<AIBehaviour>();
                    if (otherAI != null && otherAI.hasJustLittered)
                    {
                        // Mulai chasing pelaku
                        StartChasing(otherAI.transform);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(2f); // cek setiap 2 detik
        }
    }
    public void StartChasing(Transform target)
    {
        chaseTarget = target;
        isChasing = true;
    }

    //private void DetectNearbyNPCs()
    //{
    //    Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius);

    //    foreach (var hit in hits)
    //    {
    //        if (hit.gameObject != this.gameObject && hit.CompareTag(npcTag))
    //        {
    //            AIBehaviour otherAI = hit.GetComponent<AIBehaviour>();
    //            if (otherAI != null && otherAI.isInteracting == false && isInteracting == false)
    //            {

    //                // Kedua NPC masuk state Talking
    //                StartTalkingWithNPC(otherAI);
    //                otherAI.StartTalkingWithNPC(this);

    //                break;
    //            }
    //        }
    //    }
    //}

    //public void StartTalkingWithNPC(AIBehaviour otherNPC)
    //{
    //    if (isInteracting == true || otherNPC.isInteracting == true) return;

    //    isInteracting = true;
    //    animator.SetTrigger("IsExit");

    //    Vector3 direction = (transform.position - otherNPC.transform.position).normalized;
    //    Vector3 targetPos = otherNPC.transform.position + direction * distance;

    //    stateMachine.ChangeState(idleState);
    //    stateMachine.ChangeState(new TalkingState(stateMachine, idleState, this, targetPos));
    //    idleState.SetCondition("IsIdleNother");

    //    StartCoroutine(EndInteractionAfterDelay(Random.Range(15f, 20f)));
    //}

    //private IEnumerator EndInteractionAfterDelay(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    npcInter.EndInteraction();
    //}


    public void ResetIdleBools()
    {
        animator.SetBool("IsActiving", false);
        animator.SetBool("IsGuiding", false);
        animator.SetBool("IsWandering", false);
        animator.SetBool("IsIdleNother", false);
        animator.SetBool("IsTalking", false);
        animator.SetBool("IsArrangeStuff", false);
        animator.SetBool("IsThrowTrash", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsOffering", false);
        animator.SetBool("IsAnswer", false);
        animator.SetBool("IsQuestion", false);
    }
}

