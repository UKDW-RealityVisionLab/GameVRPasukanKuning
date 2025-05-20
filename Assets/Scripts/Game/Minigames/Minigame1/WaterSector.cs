using UnityEngine;

public class WaterSector : MonoBehaviour
{
    [Header("References")]
    public MeshRenderer waterRenderer;        // Visual representation (water plane)
    public GameObject ripTriggerZone;         // GameObject containing the collider/trigger
    public Material normalMaterial;
    public Material ripMaterial;

    private bool isActive = false;

    // Enable rip current visuals and collider
    public void ActivateRipCurrent()
    {
        if (waterRenderer != null && ripMaterial != null)
            waterRenderer.material = ripMaterial;

        if (ripTriggerZone != null)
            ripTriggerZone.SetActive(false);

        isActive = true;
    }

    // Disable rip current visuals and collider
    public void DeactivateRipCurrent()
    {
        if (waterRenderer != null && normalMaterial != null)
            waterRenderer.material = normalMaterial;

        if (ripTriggerZone != null)
            ripTriggerZone.SetActive(true);

        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
