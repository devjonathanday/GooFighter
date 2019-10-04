using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomToggleGroup : MonoBehaviour
{
    public string horizontalAxis;
    public string verticalAxis;
    public string submitButton;

    public float inputDelay;
    private float lastInputTime;
    
    public SelectableToggle currentSelected;

    
    
    void Start()
    {
        currentSelected.Select();
    }
    
    void Update()
    {
        if (Time.time - lastInputTime > inputDelay)
        {
            if (Input.GetAxisRaw(horizontalAxis) < 0)
            {
                currentSelected = currentSelected.SelectOnLeft();
                lastInputTime = Time.time;
            }
            if (Input.GetAxisRaw(horizontalAxis) > 0)
            {
                currentSelected = currentSelected.SelectOnRight();
                lastInputTime = Time.time;
            }
            if (Input.GetAxisRaw(verticalAxis) < 0)
            {
                currentSelected = currentSelected.SelectOnDown();
                lastInputTime = Time.time;
            }
            if (Input.GetAxisRaw(verticalAxis) > 0)
            {
                currentSelected = currentSelected.SelectOnUp();
                lastInputTime = Time.time;
            }
        }
    }
}
