using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FootFollow : MonoBehaviour
{
    Rigidbody rb;
    public float p, i, d;

    private Vector3 lastError;
    private Vector3 error;
    private Vector3 errorSum;
    public Transform target;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        error = target.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastError = error;
        error = target.position - transform.position;
        errorSum += error * Time.fixedDeltaTime;

        Vector3 correction = Vector3.zero;
        correction += error * p;
        correction += errorSum * i;
        correction += Vector3.Project(rb.velocity, rb.velocity) * d * Time.fixedDeltaTime;
        correction -= rb.velocity;

        speed = correction.magnitude;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            rb.AddForce(correction);
    }
}
