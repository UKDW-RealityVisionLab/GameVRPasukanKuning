using UnityEngine;

public class VRShakeRotation : MonoBehaviour
{
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.1f;
    private float elapsed = 0f;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void TriggerShake()
    {
        elapsed = 0f;
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    System.Collections.IEnumerator Shake()
    {
        while (elapsed < shakeDuration)
        {
            float angleX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float angleY = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.localRotation = originalRotation * Quaternion.Euler(angleX, angleY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = originalRotation;
    }
}
