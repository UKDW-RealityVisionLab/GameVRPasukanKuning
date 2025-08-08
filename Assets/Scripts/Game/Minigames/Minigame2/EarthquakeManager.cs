using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeManager : MonoBehaviour
{
    public EarthquakeHaptics haptics;
    public AudioSource earthquakeSFX;
    public AudioSource SirineSFX;
    public BuildingShakeManager buildingShakeManager;
    public WaterBlockController waterBlock;
    public float earthquakeDuration = 30f;

    [Header("Image Update")]
    public Sprite evacuationSprite;

    // Store all target Image components here
    public List<Image> signImages = new List<Image>();

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
        UpdateAllSignImages();

    }

    public void StopEarthquake()
    {
        earthquakeSFX.Stop();
        haptics.StopHapticPulse();
        waterBlock.ResetPosition();
        buildingShakeManager.StopAllBuildings();
    }

    void UpdateAllSignImages()
    {
        if (evacuationSprite == null)
        {
            Debug.LogError("Evacuation sprite is not assigned.");
            return;
        }

        foreach (Image img in signImages)
        {
            if (img != null)
            {
                img.sprite = evacuationSprite;
            }
        }

        Debug.Log("All sign images updated.");
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
