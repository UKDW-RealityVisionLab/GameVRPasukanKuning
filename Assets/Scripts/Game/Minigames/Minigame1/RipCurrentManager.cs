using UnityEngine;
public class RipCurrentController : MonoBehaviour
{
    public WaterSector[] waterSectors;

    private int[] activeRipSectors = new int[2];

    void Start()
    {
        TriggerRipCurrentsAtStart();
    }

    public void TriggerRipCurrentsAtStart()
    {
        int ripCount = Random.Range(1, 3);

        // Deactivate all first
        foreach (var sector in waterSectors)
        {
            sector.DeactivateRipCurrent();
        }

        for (int i = 0; i < ripCount; i++)
        {
            int sectorIndex;
            do
            {
                sectorIndex = Random.Range(0, waterSectors.Length);
            } while (System.Array.Exists(activeRipSectors, element => element == sectorIndex));

            activeRipSectors[i] = sectorIndex;
            waterSectors[sectorIndex].ActivateRipCurrent();
            Debug.Log($"[RipCurrentController] Rip current activated in sector {sectorIndex}");
        }
    }

    public void DeactivateAllRipCurrents()
    {
        foreach (var sector in waterSectors)
        {
            sector.DeactivateRipCurrent();
        }

        activeRipSectors = new int[2];
    }

    public bool IsRipCurrent(int index)
    {
        return index >= 0 && index < waterSectors.Length && waterSectors[index].IsActive();
    }
}
