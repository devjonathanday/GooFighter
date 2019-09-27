using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFootTarget : MonoBehaviour
{
    public Vector3 offset;
    public float timeOffset;

    [SerializeField]
    AnimationCurve walkCurveX;
    [SerializeField]
    AnimationCurve walkCurveY;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = offset + new Vector3(0, walkCurveY.Evaluate((Time.time + timeOffset) % 1) / 100, walkCurveX.Evaluate((Time.time + timeOffset) % 1) / 100);
    }
}
