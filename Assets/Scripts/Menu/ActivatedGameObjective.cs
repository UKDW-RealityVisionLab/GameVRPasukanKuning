using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatedGameObjective : MonoBehaviour
{
    [SerializeField] private GameObject objectivesPanel;  // This is the panel containing your game objectives
    [SerializeField] private Toggle objectivesToggle;

    void Start()
    {
        // Initialize toggles if assigned
        if (objectivesToggle != null)
            objectivesToggle.onValueChanged.AddListener(ToggleObjectives);
    }

    // Toggle the visibility of the game objectives
    public void ToggleObjectives(bool isOn)
    {
        if (objectivesPanel != null)
            objectivesPanel.SetActive(isOn);
    }
}
