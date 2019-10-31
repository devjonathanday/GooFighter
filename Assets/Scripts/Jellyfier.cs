using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfier : MonoBehaviour
{

    public float bounceSpeed;
    public float fallForce;
    public float forceMultiplier = 1;
    public float stiffness;
    public float roundness;
    public float falloffMultiplier;
    public bool controllable = true;

    private MeshFilter meshFilter;
    private Mesh mesh;

    JellyVertex[] jellyVerts;
    Vector3[] currentMeshVerts;
    WaitForSeconds wait = new WaitForSeconds(0.1f);
    GameManager gm;

    // Use this for initialization
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        GetVertices();

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (controllable)
            gm.jellyObjects.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVertices();
    }

    private IEnumerator JiggleTwice()
    {
        ApplyPressureToRandomPoint(fallForce, roundness);
        yield return wait;
        ApplyPressureToRandomPoint(fallForce, roundness);
    }

    public void Jiggle()
    {
        StartCoroutine(JiggleTwice());
    }

    private void UpdateVertices()
    {
        for (int i = 0; i < jellyVerts.Length; i++)
        {
            jellyVerts[i].UpdateVelocity(bounceSpeed);
            jellyVerts[i].Settle(stiffness);

            jellyVerts[i].currentVertexPosition += jellyVerts[i].currentVelocity * Time.deltaTime;
            currentMeshVerts[i] = jellyVerts[i].currentVertexPosition;
        }

        mesh.vertices = currentMeshVerts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    private void GetVertices()
    {
        jellyVerts = new JellyVertex[mesh.vertices.Length];
        currentMeshVerts = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            jellyVerts[i] = new JellyVertex(i, mesh.vertices[i], mesh.vertices[i], Vector3.zero);
            currentMeshVerts[i] = mesh.vertices[i];
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] collisionPoints = collision.contacts;
        for (int i = 0; i < collisionPoints.Length; i++)
        {
            Vector3 inputPoint = collisionPoints[i].point + (collisionPoints[i].point * 0.1f);
            ApplyPressureToPoint(inputPoint, fallForce, roundness);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            Vector3 inputPoint = contactPoints[i].point + (contactPoints[i].point * 0.1f);
            ApplyPressureToPoint(inputPoint, fallForce, roundness);
        }
    }
    public void ApplyPressureToPoint(Vector3 _point, float _pressure, float _roundness)
    {
        for (int i = 0; i < jellyVerts.Length; i++)
        {
            jellyVerts[i].ApplyPressureToVertex(transform, _point, _pressure, _roundness * falloffMultiplier);
        }
    }
    public void ApplyPressureToRandomPoint(float _pressure, float _roundness)
    {
        Vector3 randPoint = jellyVerts[Random.Range(0, jellyVerts.Length - 1)].currentVertexPosition;
        for (int i = 0; i < jellyVerts.Length; i++)
        {
            jellyVerts[i].ApplyPressureToVertex(transform, randPoint, _pressure, _roundness * falloffMultiplier);
        }
    }
}