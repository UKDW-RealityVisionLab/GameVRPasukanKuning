    using UnityEngine;
    using UnityEngine.AI;

    public class NPCFSMController : MonoBehaviour
    {
        public Transform[] waypoints;
        public NavMeshAgent agent;
        public Animator animator;

        private int currentWaypoint = 0;
        private StateMachine fsm;

        public bool isDrowning = false;

        public event System.Action OnDrowned;

        void Start()
        {
            fsm = new StateMachine();
            fsm.Initialize(new IdleState(this, fsm));
        }

        void Update()
        {
            fsm.Update();
        }

        public void GoToNextWaypoint()
        {
            if (waypoints.Length == 0) return;

            agent.SetDestination(waypoints[currentWaypoint].position);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }

        public void StopMoving()
        {
            agent.ResetPath();
        }

        public void SetWalking(bool isWalking)
        {
            if (animator != null)
                animator.SetBool("IsWalking", isWalking);
        }

        public void SetSwimming(bool swimming)
        {
            if (animator != null)
                animator.SetBool("IsSwimming", swimming);
        }

        public void TriggerDrowning()
        {
            if (animator != null)
                animator.SetTrigger("Drown");
        }

        public void InvokeOnDrowned()
        {
            OnDrowned?.Invoke();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Drown") && !isDrowning)
            {
                fsm.ChangeState(new DrownState(this, fsm));
            }

            if (other.CompareTag("Swim") && !isDrowning)
            {
                fsm.ChangeState(new SwimState(this, fsm));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Swim") && !isDrowning)
            {
                fsm.ChangeState(new IdleState(this, fsm));
            }
        }
    }
