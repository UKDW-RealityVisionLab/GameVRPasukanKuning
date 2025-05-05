using UnityEngine;

public class RipCurrentManager : MonoBehaviour
{
    public GameObject[] ripSectors; // 3 sectors (Left, Center, Right)
    public float interval = 10f; // Time between rip events
    private int currentRipIndex = -1;

    private void Start()
    {
        InvokeRepeating(nameof(ActivateRandomRipCurrent), 2f, interval);
    }

    void ActivateRandomRipCurrent()
    {
        // Deactivate previous
        if (currentRipIndex >= 0)
            ripSectors[currentRipIndex].SetActive(false);

        // Choose random sector
        currentRipIndex = Random.Range(0, ripSectors.Length);
        ripSectors[currentRipIndex].SetActive(true);
    }
}
