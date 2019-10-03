using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchScale : MonoBehaviour
{
    [Range(-1, 1)]
    public float stretch;
    public AnimationCurve XZStretch;
    public AnimationCurve YStretch;
    public Vector3 defaultScale;

    void Update()
    {
        transform.localScale = Vector3.Scale(defaultScale, new Vector3(XZStretch.Evaluate(stretch), YStretch.Evaluate(stretch), XZStretch.Evaluate(stretch)));
    }
}