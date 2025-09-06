using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeManager : MonoBehaviour
{
    [Header("References")]
    public EarthquakeHaptics haptics;
    public AudioSource earthquakeSFX;
    public AudioSource SirineSFX;
    public BuildingShakeManager buildingShakeManager;
    public WaterBlockController waterBlock;

    [Header("Settings")]
    public float startDelay;        // Time before earthquake starts
    public float earthquakeDuration;

    [Header("Image Update")]
    public Sprite evacuationSprite;

    // Store all target Image components here
    public List<Image> signImages = new List<Image>();

    private bool isEarthquakeActive = false;
    private Dictionary<Image, Sprite> originalSprites = new Dictionary<Image, Sprite>();

    void Start()
    {
        // Store original sprites for reset
        foreach (Image img in signImages)
        {
            if (img != null && !originalSprites.ContainsKey(img))
            {
                originalSprites[img] = img.sprite;
            }
        }

        StartCoroutine(EarthquakeFlow());
    }

    IEnumerator EarthquakeFlow()
    {
        // Wait before starting
        yield return new WaitForSeconds(startDelay);
        yield return StartCoroutine(StartEarthquake());
    }

    IEnumerator StartEarthquake()
    {
        if (isEarthquakeActive) yield break;
        isEarthquakeActive = true;

        // Start effects
        earthquakeSFX?.Play();        
        haptics?.TriggerHapticPulse();        
        buildingShakeManager?.ShakeAllBuildings();        

        // Wait while earthquake is active
        yield return new WaitForSeconds(earthquakeDuration);

        StopEarthquake();
        isEarthquakeActive = false;
        SirineSFX?.Play();
        waterBlock?.Sink();
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

        // Cari semua GameObject dengan tag "Iklan"
        GameObject[] iklanObjects = GameObject.FindGameObjectsWithTag("Iklan");
        foreach (var root in iklanObjects)
        {
            // Cari semua Image di hierarchy child (termasuk yg inactive jika includeInactive = true)
            Component[] images = root.GetComponentsInChildren<Image>(true);
            foreach (Image img in images)
            {
                if (img != null) { img.sprite = evacuationSprite; }
            }
            Debug.Log("Nama objek dengan tag Iklan: " + root.name);
        }

        foreach (GameObject obj in iklanObjects)
        {
            Image img = obj.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = evacuationSprite;
            }
        }

        Debug.Log("All 'Iklan' sign images updated.");
    }


#if UNITY_EDITOR
    [ContextMenu("Trigger Earthquake (Debug)")]
    private void DebugTriggerEarthquake()
    {
        StartCoroutine(StartEarthquake());
    }

    [ContextMenu("Stop Earthquake (Debug)")]
    private void DebugStopEarthquake()
    {
        StopEarthquake();
        isEarthquakeActive = false;
    }
#endif
}
