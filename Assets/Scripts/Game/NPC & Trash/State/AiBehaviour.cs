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
    public float patrolRadius = 10f;
    public bool hasJustLittered = false;
    public int playerInteractCount;

    // condition NPC
    public int activityDone;
    public int hasThirsty;
    public int isTired;
    public int wantToArrange;
    public int wantToTakePhoto;
    public int isBored;
    public string emotion = "happy";
    private string[] emotions = { "happy", "sad", "wondering" };


    public Transform targetNataBarang;
    public Transform targetSampah;
    public Transform targetCustomer;
    public Transform[] spotFoto;
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
        StartCoroutine(CycleEmotion());
    }

    public void EnterTalkingState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        // 2. Tentukan posisi interaksi
        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        // 3. Ganti state ke idle (supaya masuk substate dengan aman)
        stateMachine.ChangeState(idleState);

        // 4. Ganti state ke Talking
        stateMachine.ChangeState(new TalkingState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }
    public void EnterTalkingAngryState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new AngryState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }
    public void EnterTalkingSadState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new SadState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }
    public void EnterTalkingSadCulpritState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(wanderingState);
        stateMachine.ChangeState(new WalkTextState(stateMachine, wanderingState, this, targetPosition));
        wanderingState.SetCondition("IsWandering");
    }
    public void EnterTalkingHappyState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new HappyState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }
    public void EnterTalkingHappyCulpritState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new ThinkingState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }
    public void EnterTalkingWonderingState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(idleState);
        stateMachine.ChangeState(new WonderingState(stateMachine, idleState, this, targetPosition));
        idleState.SetCondition("IsIdleNother");
    }
    public void EnterTalkingWonderingCulpritState(Vector3 playerPosition)
    {
        isInteracting = true;
        animator.SetTrigger("IsExit");

        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + directionToPlayer * distance;

        stateMachine.ChangeState(wanderingState);
        stateMachine.ChangeState(new LaughState(stateMachine, wanderingState, this, targetPosition));
        wanderingState.SetCondition("IsWandering");
    }
    private IEnumerator CycleEmotion()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, emotions.Length);
            emotion = emotions[randomIndex];
            yield return new WaitForSeconds(10f);
        }
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
        yield return new WaitForSeconds(10f); // durasi dianggap 'baru saja' buang sampah
        hasJustLittered = false;
    }

    public Transform GetRandomChairPosition()
    {
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Chair");
        if (chairs.Length == 0) return null;

        int index = Random.Range(0, chairs.Length);
        return chairs[index].transform;
    }
    public Transform GetNearestChairPosition()
    {
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Chair");
        if (chairs.Length == 0) return null;

        Transform nearestChair = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject chair in chairs)
        {
            float distance = Vector3.Distance(currentPosition, chair.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestChair = chair.transform;
            }
        }

        return nearestChair;
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
    
    public Transform GetSpotFoto()
    {
        if (spotFoto.Length == 0) return null;

        int index = Random.Range(0, spotFoto.Length);
        return spotFoto[index].transform;
    }
    public Transform GetNearestSpotFoto()
    {
        if (spotFoto.Length == 0) return null;

        Transform nearestSpot = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform spot in spotFoto)
        {
            float distance = Vector3.Distance(currentPosition, spot.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestSpot = spot;
            }
        }

        return nearestSpot;
    }

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
        animator.SetBool("IsPlay1", false);
        animator.SetBool("IsPlay2", false);
        animator.SetBool("IsWondering", false);
        animator.SetBool("IsThinking", false);
        animator.SetBool("IsAngry", false);
    }
}

