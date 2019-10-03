using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStretch : MonoBehaviour
{
    public CapsuleCollider capsule;
    public float defaultHeight;
    public float scalar;
    public float maxStretchDistance;
    Rigidbody rBody;
    public Rigidbody connectedBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float distance = (connectedBody.position - transform.position).magnitude;
        /*
        if(distance > maxStretchDistance)
        {
            rBody.velocity = Vector3.zero;
            transform.position = (transform.position - connectedBody.position)
        }
        */
        capsule.height = distance * defaultHeight * scalar;
    }
}