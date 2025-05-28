using UnityEngine;

public class BuildingShakeManager : MonoBehaviour
{
    private BuildingShaker[] buildingShakers;

    void Awake()
    {
        // Ensure all children have a BuildingShaker
        foreach (Transform child in transform)
        {
            if (child.GetComponent<BuildingShaker>() == null)
                child.gameObject.AddComponent<BuildingShaker>();
        }

        buildingShakers = GetComponentsInChildren<BuildingShaker>();
    }

    public void ShakeAllBuildings()
    {
        foreach (var shaker in buildingShakers)
            shaker.StartShaking();
    }

    public void StopAllBuildings()
    {
        foreach (var shaker in buildingShakers)
            shaker.StopShaking();
    }



    [ContextMenu("Add BuildingShaker to All Children")]
    private void AddShakerToChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<BuildingShaker>() == null)
            {
                child.gameObject.AddComponent<BuildingShaker>();
            }
        }

        Debug.Log("Added BuildingShaker to all children!");
    }

}