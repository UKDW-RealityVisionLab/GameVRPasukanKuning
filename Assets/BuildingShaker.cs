using UnityEngine;

public class BuildingShaker : MonoBehaviour
{
    public float shakeIntensity = 0.1f;
    public float shakeSpeed = 10f;

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            Vector3 offset = Random.insideUnitSphere * shakeIntensity;
            transform.localPosition = originalPosition + offset;
        }
    }

    public void StartShaking()
    {
        isShaking = true;
    }

    public void StopShaking()
    {
        isShaking = false;
        transform.localPosition = originalPosition;
    }
}
