using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class HealthBar
{
    public RectTransform MainScalar;
    public RectTransform LossScalar;
    float StartHealth = 100;
    float EndHealth = 100;

    float TimerVar;
    public float Timer
    {
        get { return TimerVar; }
        set { TimerVar = Mathf.Clamp(value, 0, 1); }
    }

    public void Update(int _Health, int _PreviousHealth)
    {
        //If the TempEndHealth is not the new Health
        if(EndHealth != _Health)
        {
            //Set the starting position of the health to the current position
            StartHealth = Mathf.Lerp((float)StartHealth, (float)EndHealth, Timer);
            //Set the Temp Health to the new health
            EndHealth = _Health;
            //Reset the current timer
            ResetTimer();
        }
        //Change the Main health to the New health
        MainScalar.localScale = new Vector3((float)_Health / 100, 1, 1);
        //Update the timer
        TimerUpdate();
        //Lerp the Loss health to the new Low Health
        LossScalar.localScale = new Vector3(Mathf.Lerp((float)StartHealth / 100, (float)_Health / 100, Timer), 1, 1);
    }
    void ResetTimer()
    {
        Timer = 0;
    }
    void TimerUpdate()
    {
        Timer += Time.deltaTime;
    }
}

public class UpdateUI : MonoBehaviour
{
    #region Number Meshes
    [Header("Number Meshes")]
    public GameObject[] NumberObjects = new GameObject[3];//1-3
    public GameObject[] PlayerOneScoreObjects = new GameObject[3];//Display for player one's score
    public GameObject[] PlayerTwoScoreObjects = new GameObject[3];//Display for players two's score
    #endregion

    #region ManagerControllers
    GameManager Manager;//Static Game manager
    [Header("Controllers")]
    public PlayerController PController;
    #endregion

    #region Displays
    [Header("UIDisplays")]
    public TextMeshProUGUI TimerDisplay;//Timer Text display 
    #endregion

    #region HealthBars
    [Header("Health Display")]
    public HealthBar PlayerOneHealth;
    public HealthBar PlayerTwoHealth;
    #endregion

    #region Preferences
    [Header("Different Preferences")]
    public bool DisplayTimeInSeconds;
    #endregion

    void Start()
    {
        //Attempt to find GameManager
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //If no game manager, create another
        if (Manager == null) Manager = GameManager.CreateMissingManager().GetComponent<GameManager>();
    }

    void Update()
    {
        //Displays correct Object for the Round number
        DisplayRoundNumber();
        //Displays both of the players Wins
        DisplayWins();
        //Displays the round timer if the object exists
        if (TimerDisplay != null) DisplayTimer();
        //Updates PlayerOne's health display
        PlayerOneHealth.Update(PController.PlayerOne.GetHealth(), PController.PlayerOne.GetPreviousHealth());
        PlayerTwoHealth.Update(PController.PlayerTwo.GetHealth(), PController.PlayerTwo.GetPreviousHealth());
        if(Input.GetKeyDown(KeyCode.Space))PController.PlayerOne.DamagePlayer(10);
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
        PlayerOneScoreObjects[(Manager.GetPlayerScore(1) < PlayerOneScoreObjects.Length) ? Manager.GetPlayerScore(1) : PlayerOneScoreObjects.Length - 1].SetActive(true);
        PlayerTwoScoreObjects[(Manager.GetPlayerScore(2) < PlayerTwoScoreObjects.Length) ? Manager.GetPlayerScore(2) : PlayerTwoScoreObjects.Length - 1].SetActive(true);
    }
}
