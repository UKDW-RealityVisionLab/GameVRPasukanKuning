using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance;

    public GameObject chatBubblePrefab;

    private void Awake()
    {
        Instance = this;
    }
}
