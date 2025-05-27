using System.Collections;
using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    public EarthquakeHaptics haptics;
    public VRShakeRotation vrShake;
    public AudioSource earthquakeSFX;

    void Start()
    {
        // StartCoroutine(StartEarthquake());
    }

    IEnumerator StartEarthquake()
    {
        yield return new WaitForSeconds(1f);  // Delay opsional sebelum gempa
        earthquakeSFX.Play();
        haptics.TriggerHapticPulse();
        vrShake.TriggerShake();
    }

#if UNITY_EDITOR
    [ContextMenu("Trigger Earthquake (Debug)")]
    private void DebugTriggerEarthquake()
    {
        StartCoroutine(StartEarthquake());  // <- FIXED
    }
#endif
}
