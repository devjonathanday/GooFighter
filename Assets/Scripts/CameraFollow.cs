using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool active;
    public Transform focusA, focusB;
    public Vector3 offset;
    public float moveLerp;
    public float minimumZoom;
    public float zoomScalar;
    public Vector3 axisScalar;
    
    void LateUpdate()
    {
        if (active)
        {
            float distance = Vector3.Scale((focusA.position - focusB.position), axisScalar).magnitude; //hotdog
            Vector3 midPoint = (focusA.position + focusB.position) / 2;
            float zoom = Mathf.Max(distance * zoomScalar, minimumZoom);
            transform.position = Vector3.Lerp(transform.position, midPoint + (offset * zoom), moveLerp);
        }
    }
}
