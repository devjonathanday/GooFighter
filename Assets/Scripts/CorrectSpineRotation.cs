using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSpineRotation : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    private Quaternion targetRot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRot = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * speed));
    }
}
