using UnityEngine;

public class SocketController : MonoBehaviour
{
    public GameObject targetObject;
    public bool activateWhenBothOccupied = true; // Check this in the Inspector

    private bool isSocketAOccupied = false;
    private bool isSocketBOccupied = false;

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
    }
}
