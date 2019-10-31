using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuManager : MonoBehaviour
{
    GameManager Manager;

    public GameObject Page1;
    public GameObject Page2;


    public int currentPage;
    int CurrentPage
    {
        set
        {
            currentPage = Mathf.Clamp(value, 0, 1);
        }
        get
        {
            return currentPage;
        }
    }

    public Vector3 LeftHidden;
    public Vector3 RightHidden;

    Vector3 Page1Start;
    Vector3 Page2Start;

    float LerpTimer;

    private void Start()
    {
        Manager = GameManager.FindManager();
    }

    private void Update()
    {
        SwitchBetweenPages();
    }

    void SwitchBetweenPages()
    {
        if (Manager.GetAxis(1, "Horizontal") * 2 + Manager.GetAxis(2, "Horizontal") < 0)
        {
            CurrentPage -= 1;
            Page1Start = Page1.transform.position;
            LerpTimer = Time.time;
        }
        if (Manager.GetAxis(1, "Horizontal") * 2 + Manager.GetAxis(2, "Horizontal") > 0)
        {
            CurrentPage += 1;
            Page2Start = Page2.transform.position;
            LerpTimer = Time.time;
        }
    }

    void PositionPages()
    {
        if(CurrentPage == 1)
        {
            Page1.transform.position = Vector3.Lerp(Page1Start, Vector3.zero, Mathf.Clamp(Time.time - LerpTimer, 0, 1));
            Page2.transform.position = Vector3.Lerp(Page2Start, RightHidden, Mathf.Clamp(Time.time - LerpTimer, 0, 1));
        }
        else if (CurrentPage == 2)
        {
            Page1.transform.position = Vector3.Lerp(Page1Start, LeftHidden, Mathf.Clamp(Time.time - LerpTimer, 0, 1));
            Page2.transform.position = Vector3.Lerp(Page2Start, Vector3.zero, Mathf.Clamp(Time.time - LerpTimer, 0, 1));
        }
    }
} 
