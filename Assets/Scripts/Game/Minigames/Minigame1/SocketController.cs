using UnityEngine;
using System;

public class SocketController : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject ripCurrentObject;
    public bool activateWhenBothOccupied = true; // Check this in the Inspector

    private bool isSocketAOccupied = false;
    private bool isSocketBOccupied = false;
    public static event Action<SocketController> OnRipCurrentBecameActive;
    public static event Action<SocketController> OnRipCurrentBecameInactive;

    public enum SocketID { A, B }

    public void SetSocketOccupied(SocketID socket, bool isOccupied)
    {
        switch (socket)
        {
            case SocketID.A:
                isSocketAOccupied = isOccupied;
                break;
            case SocketID.B:
                isSocketBOccupied = isOccupied;
                break;
        }

        UpdateObjectState();
        
        
    }

    private void UpdateObjectState()
    {
        bool bothOccupied = isSocketAOccupied && isSocketBOccupied;
        targetObject.SetActive(activateWhenBothOccupied ? bothOccupied : !bothOccupied);
        if (bothOccupied)
        {
            CheckRipCurrentState();
        }
        
    }


    private void CheckRipCurrentState()
    {
        if (ripCurrentObject == null || targetObject.activeSelf) return;

        if (ripCurrentObject.activeSelf)
        {
            // Rip current is ON (dangerous)
            OnRipCurrentBecameActive?.Invoke(this);
        }
        else
        {
            // Rip current is OFF (safe)
            OnRipCurrentBecameInactive?.Invoke(this);
        }
    }
}
