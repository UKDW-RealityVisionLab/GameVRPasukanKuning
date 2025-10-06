using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider sliderVal;
    [SerializeField] private Text textVal;
    public int hargaTambah;
    // Start is called before the first frame update
    void Start()
    {
        sliderVal.onValueChanged.AddListener((v) =>
        {
            textVal.text = ((int)v).ToString();
        });
    }

    public int GetHargaTambah()
    {
        // langsung return nilai slider sebagai int
        return (int)sliderVal.value;
    }

}
