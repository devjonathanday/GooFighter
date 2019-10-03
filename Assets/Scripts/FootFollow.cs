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

    public string[] inputAxes;

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

        Debug.DrawRay(transform.position, Vector3.Project(rb.velocity, rb.velocity) * d * Time.fixedDeltaTime, Color.red);
        Debug.DrawRay(transform.position, rb.velocity, Color.blue);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Sign(error.x), Mathf.Sign(error.y), Mathf.Sign(error.z)));

        Vector3 correction = Vector3.zero;
        correction += error * p;
        correction += errorSum * i;
        correction += Vector3.Project(rb.velocity, rb.velocity) * d * Time.fixedDeltaTime;

        speed = correction.magnitude;

        if (GetInput())
            rb.AddForce(correction * 0.9f);
    }
    bool GetInput()
    {
        for (int i = 0; i < inputAxes.Length; i++)
        {
            if (Input.GetAxis(inputAxes[i]) != 0)
                return true;
        }
        return false;
    }
}
