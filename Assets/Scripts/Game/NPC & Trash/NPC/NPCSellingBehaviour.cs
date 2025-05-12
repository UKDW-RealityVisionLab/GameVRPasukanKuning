using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;


public class NPCSellingBehaviour : MonoBehaviour
{
    [SerializeField] private Transform sellPoint;
    [SerializeField] private Transform nataBarangPoint;
    [SerializeField] private Transform buangSampahPoint;
    [SerializeField] public Animator animator;
    [SerializeField] private GameObject uiNPC;

    private NavMeshAgent agent;

    private float stateTimer = 0f;
    private float stateDuration = 5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }
}
