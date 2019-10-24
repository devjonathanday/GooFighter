using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBall : MonoBehaviour
{
    public enum BALLTYPE { Solid, Stripes }
    public enum BALLCOLOR { Red,Blue,Green, Yellow, Black, White}

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
            case BALLCOLOR.Yellow:
                MyRend.materials[0].color = Color.yellow;
                break;
            case BALLCOLOR.Black:
                MyRend.materials[0].color = Color.black;
                break;
            case BALLCOLOR.White:
                MyRend.materials[0].color = Color.white;
                break;
            default:
                break;
        }
        if (BallType == BALLTYPE.Solid) Destroy(MyRend.materials[2]);
        else Destroy(MyRend.materials[1]);
    }
}
