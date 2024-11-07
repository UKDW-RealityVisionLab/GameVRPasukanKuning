using UnityEngine;

public class TruckController : MonoBehaviour
{
    public bool canResumePatrol = false; // Variable to control patrol resumption

    // You can call this method to change the value of canResumePatrol
    public void AllowTruckPatrol()
    {
        canResumePatrol = true; // Set to true to allow the truck to resume patrolling
    }

        public void DisallowTruckPatrol()
    {
        canResumePatrol = false; // Set to true to allow the truck to resume patrolling
    }
}
