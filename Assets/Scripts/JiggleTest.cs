using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleTest : MonoBehaviour
{
    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("do jiggle");
            r.material.SetFloat("_WobbleStart", Time.time);
        }
    }
}
