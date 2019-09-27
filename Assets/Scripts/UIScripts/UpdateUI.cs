using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [Header("Number Meshes")]
    public GameObject[] NumberObjects = new GameObject[3];//1-3
    public GameObject[] PlayerOneScoreObjects = new GameObject[3];//Display for player one's score
    public GameObject[] PlayerTwoScoreObjects = new GameObject[3];//Display for players two's score

    [Header("GameManager")]
    GameManager Manager;

    [Header("UIDisplays")]
    public TextMeshProUGUI TimerDisplay;
    //public TextMeshProUGUI 

    [Header("Different Preferences")]
    public bool DisplayTimeInSeconds;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        DisplayRoundNumber();
        DisplayWins();
        if (TimerDisplay != null) DisplayTimer();
    }

    void DisplayRoundNumber()
    {
        //Set all the round numbers to be hidden
        for(int i = 0; i < NumberObjects.Length; i++)
        {
            NumberObjects[i].SetActive(false);
        }
        //Set the current round number to appear
        //If the round is greator than the max rounds, the display is the max rounds
        NumberObjects[(Manager.GetRound() < NumberObjects.Length) ? Manager.GetRound() : NumberObjects.Length - 1].SetActive(true);
    }
    void DisplayTimer()
    {
        //Display the time left for the round
        if(DisplayTimeInSeconds)
        {
            TimerDisplay.text = ((int)Manager.GetRoundTime()).ToString();
        }
        //Timer needs to count in seconds, but display in minutes and seconds
        else
        {
            //Displays the timer as Minutes and then seconds
            TimerDisplay.text = (((int)Manager.GetRoundTime()) / 60).ToString();

            //If the Secounds == 0
            if((((int)Manager.GetRoundTime()) % 60) == 0)
            {
                //Display them as two 0's rather than one
                TimerDisplay.text += ":" + "00";
            }
            else
            {
                //Else display the seconds
                TimerDisplay.text += ":";
                //If the time is less than 10 seconds
                if ((((int)Manager.GetRoundTime()) % 60) < 10)
                {
                    //Add a zero before the number
                    TimerDisplay.text += "0" + (((int)Manager.GetRoundTime()) % 60);
                }
                else
                {
                    //Timer is displayed as is
                    TimerDisplay.text += (((int)Manager.GetRoundTime()) % 60);
                }
            }
            
        }
    }

    void DisplayWins()
    {
        //Loop through all the objects in PlayerOneScoreObjects and PlayerTwoScoreObjects
        for (int i = 0; i < PlayerOneScoreObjects.Length; i++)
        {
            //Set them all their activness to false
            PlayerOneScoreObjects[(i < PlayerOneScoreObjects.Length) ? i : PlayerOneScoreObjects.Length].SetActive(false);
            PlayerTwoScoreObjects[(i < PlayerOneScoreObjects.Length) ? i : PlayerOneScoreObjects.Length].SetActive(false);
        }
        //Set the correct one to true
        PlayerOneScoreObjects[Manager.GetPlayerScore(1)].SetActive(true);
        PlayerTwoScoreObjects[Manager.GetPlayerScore(2)].SetActive(true);
    }
}
