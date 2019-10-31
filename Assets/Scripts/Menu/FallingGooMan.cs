using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGooMan : MonoBehaviour
{
    public float respawnYPos;
    public Vector3 startPos;
    public Vector2 xRange;
    public Vector2 zRange;
    public Rigidbody rBody;
    public float initalTorque;

    void Start()
    {
        startPos = transform.position;
        Reset();
    }

    void Update()
    {
        if(transform.position.y < respawnYPos)
            Reset();
    }

    private void Reset()
    {
        transform.position = new Vector3(Random.Range(xRange.x, xRange.y), startPos.y, Random.Range(zRange.x, zRange.y));
        rBody.velocity = Vector3.zero;
        Vector3 randomRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rBody.AddTorque(randomRotation * initalTorque);
    }
}