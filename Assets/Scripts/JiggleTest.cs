using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleTest : MonoBehaviour
{
    Jellyfier r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Jellyfier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            r.ApplyPressureToRandomPoint(r.fallForce);
        }
    }
}
