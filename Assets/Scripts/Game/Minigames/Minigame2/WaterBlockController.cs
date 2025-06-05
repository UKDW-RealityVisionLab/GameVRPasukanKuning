using UnityEngine;

public class WaterBlockController : MonoBehaviour
{
    public float sinkAmount = 2f; // how far down to move
    public float sinkSpeed = 1f;  // speed of sinking movement

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isSinking = false;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.down * sinkAmount;
    }

    void Update()
    {
        if (isSinking)
        {
            // Move down smoothly
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, sinkSpeed * Time.deltaTime);
        }
        else
        {
            // Move back to initial position smoothly
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, sinkSpeed * Time.deltaTime);
        }
    }

    public void Sink()
    {
        isSinking = true;
    }

    public void ResetPosition()
    {
        isSinking = false;
    }
}
