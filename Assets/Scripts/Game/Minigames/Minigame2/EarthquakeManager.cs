using System.Collections;
using UnityEngine;

public class EarthquakeManager : MonoBehaviour
{
    public EarthquakeHaptics haptics;
    public AudioSource earthquakeSFX;
    public AudioSource SirineSFX;
    public BuildingShakeManager buildingShakeManager;
    public WaterBlockController waterBlock;
    public float earthquakeDuration = 30f;

    void Start()
    {
        StartCoroutine(StartEarthquake());
    }

    IEnumerator StartEarthquake()
    {
        yield return new WaitForSeconds(earthquakeDuration);
        earthquakeSFX.Play();
        SirineSFX.Play();
        haptics.TriggerHapticPulse();
        waterBlock.Sink();
        buildingShakeManager.ShakeAllBuildings();

    }

    public void StopEarthquake()
    {
        earthquakeSFX.Stop();
        haptics.StopHapticPulse();
        waterBlock.ResetPosition();
        buildingShakeManager.StopAllBuildings();
    }

#if UNITY_EDITOR
    [ContextMenu("Trigger Earthquake (Debug)")]
    private void DebugTriggerEarthquake()
    {
        StartCoroutine(StartEarthquake());  // <- FIXED
    }
    [ContextMenu("Stop Earthquake (Debug)")]
    private void DebugStopEarthquake()
    {
        StopEarthquake();
    }
#endif
}
