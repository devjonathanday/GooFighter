using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingGooMan : MonoBehaviour
{
    public float desiredZPos;
    public Transform hand;
    
    void Update()
    {
        hand.position = Camera.main.ScreenToWorldPoint(new Vector3(-Input.mousePosition.x, -Input.mousePosition.y, desiredZPos));
    }
}