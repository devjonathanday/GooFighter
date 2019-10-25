using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoolBall : MonoBehaviour
{
    public enum BALLTYPE { Solid, Stripes }
    public enum BALLCOLOR { Red,Blue,Green, Yellow, Orange, Purple, Brown, Black, White}

    public BALLTYPE BallType;
    public BALLCOLOR BallColor;
    [Range(0,15)]
    public int BallNumber;

    Renderer MyRend;
    TextMeshPro NumberText;

    void Start()
    {
        MyRend = GetComponent<Renderer>();
        NumberText = GetComponentInChildren<TextMeshPro>();

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
            case BALLCOLOR.Purple:
                MyRend.materials[0].color = new Color(.3f,0,.6f);
                break;
            case BALLCOLOR.Orange:
                MyRend.materials[0].color = new Color(1, .5f, 0);
                break;
            case BALLCOLOR.Brown:
                MyRend.materials[0].color = new Color(.3f,0,0);
                break;
            default:
                break;
        }
        if (BallType == BALLTYPE.Solid) Destroy(MyRend.materials[2]);
        else Destroy(MyRend.materials[1]);

        NumberText.text = BallNumber.ToString();
        if (BallNumber == 0) NumberText.text = "";


    }
}
