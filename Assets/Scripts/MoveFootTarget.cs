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
    float timer;

    void Start()
    {
        offset = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }
    void FixedUpdate()
    {
        transform.localPosition = offset + new Vector3(0, (walkCurveY.Evaluate((timer + timeOffset) % 1) - distanceLimit) / 100, (walkCurveX.Evaluate((timer + timeOffset) % 1) - distanceLimit) / 100);
    }
}
