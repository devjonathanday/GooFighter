using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleTest : MonoBehaviour
{
    Jellyfier r;
    WaitForSeconds wait = new WaitForSeconds(0.1f);
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
            StartCoroutine(JiggleTwice());
        }
    }

    private IEnumerator JiggleTwice()
    {
        r.ApplyPressureToRandomPoint(r.fallForce, r.roundness);
        yield return wait;
        r.ApplyPressureToRandomPoint(r.fallForce, r.roundness);
    }
}
