using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CustomToggleGroup : MonoBehaviour
{
    public string horizontalAxis;
    public string verticalAxis;
    public string submitButton;
    public string cancelButton;

    public float inputDelay;
    private float lastInputTime;
    
    public SelectableToggle currentSelected;
    public TextMeshProUGUI readyText;
    public GameManager GM;
    public bool ready;
    
    void Start()
    {
        currentSelected.Select();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
        if (Input.GetButtonDown(submitButton))
        {
            ready = true;
            readyText.text = "Ready!";
            readyText.color = Color.green;
        }
        if (Input.GetButtonDown(cancelButton))
        {
            ready = false;
            readyText.text = "Deciding...";
            readyText.color = Color.red;
        }
    }
}
