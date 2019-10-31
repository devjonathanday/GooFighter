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

    GameManager gm;
    Vector3 offset;
    float timer;

    void Start()
    {
        gm = GameManager.FindManager();
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
        if (transform.root.name == "Player1")
        {
            return (gm.GetAxis(1, "Horizontal") != 0 || gm.GetAxis(1, "Vertical") != 0);
        }
        if (transform.root.name == "Player2")
        {
            return (gm.GetAxis(2, "Horizontal") != 0 || gm.GetAxis(2, "Vertical") != 0);
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
