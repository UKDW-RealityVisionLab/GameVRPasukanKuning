using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatedMinimap : MonoBehaviour
{
    [SerializeField] private RawImage minimapImage;
    [SerializeField] private Toggle minimapToggle;

    void Start()
    {
        if (minimapToggle != null)
        {
            minimapToggle.onValueChanged.AddListener(ToggleMinimap);
        }
    }

    // This method is called whenever the toggle is changed
    public void ToggleMinimap(bool isOn)
    {
        if (minimapImage != null)
        {
            minimapImage.gameObject.SetActive(isOn);
        }
    }
}
