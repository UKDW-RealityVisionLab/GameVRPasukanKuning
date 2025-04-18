using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlowly : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // units per second

    void Update()
    {
        // Move upward every frame at moveSpeed
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
