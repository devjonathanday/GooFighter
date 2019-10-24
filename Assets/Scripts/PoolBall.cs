using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBall : MonoBehaviour
{
    public enum BALLTYPE { Solid, Stripes }
    public enum BALLCOLOR { Red,Blue,Green}

    public BALLTYPE BallType;
    public BALLCOLOR BallColor;

    Renderer MyRend;

    void Start()
    {
        MyRend = GetComponent<Renderer>();

        switch (BallColor)
        {
            case BALLCOLOR.Red:
                MyRend.materials[0].color = Color.red;
                break;
            case BALLCOLOR.Blue:
                MyRend.materials[0].color = Color.blue;
                break;
            case BALLCOLOR.Green:
                MyRend.materials[0].color = Color.green;
                break;
            default:
                break;
        }
        if (BallType == BALLTYPE.Solid) MyRend.materials[0].SetTexture("_MainTex", null);
    }
}
