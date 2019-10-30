using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomToggleGroup : MonoBehaviour
{
    //public string horizontalAxis;
    //public string verticalAxis;
    //public string submitButton;
    //public string cancelButton;
    public int IndexNumber;

    public float inputDelay;
    private float lastInputTime;

    public SelectableToggle currentSelected;
    public TextMeshProUGUI readyText;
    public GameObject readyStatus;
    public GameObject cancelStatus;
    public GameManager GM;

    public GameObject buttonOrganizer;
    public CustomToggleGroup otherToggleGroup;

    public bool ready;

    [Header("Scene Management")]
    public string PreviousScene;

    private void Awake()
    {
        GM = GameManager.FindManager();
    }
    void Start()
    {
        currentSelected.Select();
    }

    void Update()
    {
        if (!ready && Time.time - lastInputTime > inputDelay)
        {
            if (GM.GetAxis(IndexNumber, "Horizontal") < 0)
            {
                currentSelected = currentSelected.SelectOnLeft();
                lastInputTime = Time.time;
            }
            if (GM.GetAxis(IndexNumber, "Horizontal") > 0)
            {
                currentSelected = currentSelected.SelectOnRight();
                lastInputTime = Time.time;
            }
            if (GM.GetAxis(IndexNumber, "Vertical") < 0)
            {
                currentSelected = currentSelected.SelectOnDown();
                lastInputTime = Time.time;
            }
            if (GM.GetAxis(IndexNumber, "Vertical") > 0)
            {
                currentSelected = currentSelected.SelectOnUp();
                lastInputTime = Time.time;
            }
        }

        if (GM.GetButtonDown(IndexNumber, "Submit"))
        {
            if (!otherToggleGroup.ready || currentSelected.colorID != otherToggleGroup.currentSelected.colorID)
            {
                ready = true;
                readyStatus.SetActive(false);
                cancelStatus.SetActive(true);
                readyText.text = string.Empty;
                buttonOrganizer.SetActive(false);
            }
            else
            {
                readyText.text = "ALREADY TAKEN!";
            }
        }
        if (GM.GetButtonDown(IndexNumber, "Cancel"))
        {
            if (ready)
            {
                ready = false;
                readyStatus.SetActive(true);
                cancelStatus.SetActive(false);
                readyText.text = string.Empty;
                buttonOrganizer.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(PreviousScene);
            }
        }
    }
}