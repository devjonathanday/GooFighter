using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    public PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 2, ForceMode.VelocityChange);
            PC.HeadForce = Vector3.zero;
            PC.BodyForce = Vector3.zero;
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            PC.HeadForce = new Vector3(0, .75f, 0);
            PC.BodyForce = new Vector3(0, 1.5f, 0);
        }
    }
}