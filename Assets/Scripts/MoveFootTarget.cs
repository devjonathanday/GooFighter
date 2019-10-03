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
    public string[] inputAxes;

    void Start()
    {
        offset = transform.localPosition;
    }

    void Update()
    {
        if (GetInput())
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
        transform.localPosition = offset + new Vector3(0, (walkCurveY.Evaluate((timer + (GetInput() ? timeOffset : 0)) % 1)) / 100, (walkCurveX.Evaluate((timer + (GetInput() ? timeOffset : 0)) % 1)) / 100);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
