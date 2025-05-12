using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCVisionCone : MonoBehaviour
{
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example
    public int VisionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    public bool playerInSigth;
    //Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
    //for the ones that you dont understand dont worry, just follow along
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
    }

    void Update()
    {
        DrawVisionCone();//calling the vision cone function everyframe just so the cone is updated every frame
        playerInSigth = false; // reset first
        Collider[] hits = Physics.OverlapSphere(transform.position, VisionRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player")) // Make sure player has the "Player" tag
            {
                DetectPlayer(hit);
            }
        }
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
            }


            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }

    public void DetectPlayer(Collider other)
    {
        Vector3 directionToTarget = (other.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, other.transform.position);

        // Check if the player is within range
        if (distanceToTarget > VisionRange)
        {
            playerInSigth = false;
            return;
        }

        // Check if the player is within the vision angle
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        if (angleToTarget > VisionAngle * Mathf.Rad2Deg / 2f)
        {
            playerInSigth = false;
            return;
        }

        // Raycast to check if there's an obstacle between NPC and player
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget, VisionObstructingLayer))
        {
            playerInSigth = false;
            return;
        }

        // If all checks passed, player is in sight
        playerInSigth = true;
    }
}
