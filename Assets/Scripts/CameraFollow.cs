using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool active;
    public Transform focusA, focusB;
    public Vector3 offset;
    public float moveSpeed;
    
    void LateUpdate()
    {
        if (active)
        {
            Vector3 midPoint = (focusA.position + focusB.position) / 2;
            transform.position = Vector3.Lerp(transform.position, midPoint + offset, moveSpeed);
        }
    }
}
