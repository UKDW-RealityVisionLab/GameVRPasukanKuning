using UnityEngine;

public class SocketStatusReporter : MonoBehaviour
{
    public SocketController.SocketID socketID;
    public SocketController socketController;

    public void OnSocketOccupied()
    {
        socketController.SetSocketOccupied(socketID, true);
    }

    public void OnSocketEmptied()
    {
        socketController.SetSocketOccupied(socketID, false);
    }
}
