using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStretch : MonoBehaviour
{
    public CapsuleCollider capsule;
    public float defaultHeight;
    public float scalar;
    public Rigidbody connectedBody;
    
    void Update()
    {
        capsule.height = (connectedBody.position - transform.position).magnitude * defaultHeight * scalar;
    }
}