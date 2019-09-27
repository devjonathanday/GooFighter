using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFootTarget : MonoBehaviour
{
    public float timeOffset;
    public float distanceLimit;

    [SerializeField]
    AnimationCurve walkCurveX = new AnimationCurve();
    [SerializeField]
    AnimationCurve walkCurveY = new AnimationCurve();

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = offset + new Vector3(0, (walkCurveY.Evaluate((Time.time + timeOffset) % 1) - distanceLimit) / 100, (walkCurveX.Evaluate((Time.time + timeOffset) % 1) - distanceLimit) / 100);
    }
}
