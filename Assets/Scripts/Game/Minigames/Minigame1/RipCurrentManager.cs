using UnityEngine;

public class RipCurrentController : MonoBehaviour
{
    public MeshRenderer[] waterSectors;      // 3 water planes
    public Material normalMaterial;
    public Material ripMaterial;

    private int[] activeRipSectors = new int[2]; // Max 2 rip current sectors at once

    void Start()
    {
        TriggerRipCurrentsAtStart();
    }

    // Method to activate rip current in random sectors at start
    public void TriggerRipCurrentsAtStart()
    {
        // Randomly select 1 or 2 sectors to activate
        int ripCount = Random.Range(1, 3);  // Randomly choose 1 or 2 sectors

        // Set all sectors to normal initially
        for (int i = 0; i < waterSectors.Length; i++)
        {
            waterSectors[i].material = normalMaterial;
        }

        // Randomly activate the selected sectors
        for (int i = 0; i < ripCount; i++)
        {
            int sectorIndex;
            do
            {
                sectorIndex = Random.Range(0, waterSectors.Length); // Random sector
            } while (System.Array.Exists(activeRipSectors, element => element == sectorIndex)); // Ensure no duplicate

            activeRipSectors[i] = sectorIndex;
            waterSectors[sectorIndex].material = ripMaterial; // Activate rip current in the selected sector
            Debug.Log($"[RipCurrentController] Rip current activated in sector {sectorIndex}");
        }
    }

    // Check if a sector has a rip current
    public bool IsRipCurrent(int sectorIndex)
    {
        return System.Array.Exists(activeRipSectors, element => element == sectorIndex);
    }
}
