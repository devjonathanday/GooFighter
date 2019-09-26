using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float offsetHeight;
    public float lookAngle;
    public float rotSpeed;
    public float distance;

    // Update is called once per frame
    void LateUpdate()
    {
        // 3D Camera
        transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.rotation, 1);
        transform.eulerAngles = new Vector3(lookAngle, transform.eulerAngles.y + (Input.GetKey(KeyCode.E) ? 180 : 0), transform.eulerAngles.z);
        transform.position = followTarget.position - (transform.forward * distance) + Vector3.up * offsetHeight;

        // 2D Camera
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(followTarget.position - transform.position), 1);
        //transform.eulerAngles = new Vector3(lookAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        //transform.position = followTarget.position + offset;
    }
}
