using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GAMESTATE { MainMenu, Rounds, EndOfRound, TransitionRound}

public class GameManager : MonoBehaviour
{
    static GameManager Manager;//Singletoness

    public GAMESTATE CurrentState;//Current state of the game
    int Round = 0;//Which round is current
    public bool HasCheckedRoundNumber = false;//Checks to see if the 

    int Player1Score = 0;//Score For Player One
    int Player2Score = 0;//Score For Player Two
    
    public int Player1ColorID = 0;
    public int Player2ColorID = 0;

    //0 = Red
    //1 = Yellow
    //2 = Green
    //3 = Blue
    //4 = Pink
    //5 = Black

    float MaxRoundTimer = 180.0f;//Reset time for the Timer
    float RoundTimer;//Round timer


    void Start()
    {
        //singleton
        if (Manager == null) Manager = this;
        else Destroy(gameObject);

        //GameManager Don't destroy from scene to scene
        DontDestroyOnLoad(this);

        //Inits the timer to to max time
        ResetRoundTimer();
    }

    void Update()
    {
        TimerUpdate();
        //CHEAT();
    }

    //Round functions
    public void NextRound()
    {
        HasCheckedRoundNumber = false;
        Round++;
    }
    public int GetRound()
    {
        return Round;
    }
    public void ResetRounds()
    {
        Round = 0;
    }

    //Timer functions
    public float GetRoundTime()
    {
        return RoundTimer;
    }
    void RoundWaitingPeriod()
    {
        //Sets the round to a specific round waiting timer (Default 5 seconds)
        RoundTimer = 5.0f;
    }
    public void ResetRoundTimer()
    {
        //Sets the timer to the max timer
        RoundTimer = MaxRoundTimer;
    }

    //Gamestates functions
    public void SetGameState(GAMESTATE _NewState)
    {
        CurrentState = _NewState;

        //If the Current state is the end of round
        if (CurrentState == GAMESTATE.TransitionRound)
        {
            //NextRound
            NextRound();
            //Reset the current round for the next play
            ResetingRound();
            //Resets the state to be the rounds
            CurrentState = GAMESTATE.Rounds;
        }
    }
    public void SetGameState(GAMESTATE _NewState, int _PlayerNumber)
    {
        CurrentState = _NewState;

        //If the Current state is the end of round
        if (CurrentState == GAMESTATE.EndOfRound)
        {
            //Record which player won
            IncreasePlayerScore(_PlayerNumber);
            //Set Round ending timer
            RoundWaitingPeriod();
        }
    }
    public GAMESTATE GetState()
    {
        return CurrentState;
    }

    //Reseting the team for starting the next round
    void ResetingRound()
    {
        //Resets the round timer
        ResetRoundTimer();
        //Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Update the timer
    void TimerUpdate()
    {
        //Count down the timer
        RoundTimer -= Time.deltaTime;

        //If the timer has ran out
        if (RoundTimer <= 0.0f)
        {
            //Check states
            if (GetState() == GAMESTATE.Rounds)
            {
                //Set the State to end of round
                SetGameState(GAMESTATE.EndOfRound);
                //Set the timer to cound down again
                RoundWaitingPeriod();
            }
            else if (GetState() == GAMESTATE.EndOfRound)
            {
                //Set state to the transition state
                SetGameState(GAMESTATE.TransitionRound);
            }
        }
    }

    //Player Score functions
    void IncreasePlayerScore(int _PlayerNumber)
    {
        //If player 1 won, give them a point
        if (_PlayerNumber == 1) Player1Score++;
        //If player 2 won, give the a point
        else Player2Score++;
    }
    public int GetPlayerScore(int _PlayerNumber)
    {
        //Return Correct player score
        //If player does not exist, return 0
        return (_PlayerNumber == 1) ? Player1Score : 
               (_PlayerNumber == 2) ? Player2Score : 
               0;
    }

    void CHEAT()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetGameState(GAMESTATE.EndOfRound, 1);//Player one wins
            RoundWaitingPeriod();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SetGameState(GAMESTATE.EndOfRound, 2);//Player two wins
            RoundWaitingPeriod();
        }
    }

    public static GameObject CreateMissingManager()
    {
        GameObject NewManager = new GameObject();
        NewManager.tag = "GameManager";
        NewManager.AddComponent<GameManager>();
        return NewManager;
    }

    public void ChangePlayer1Color(int colorID)
    {
        Player1ColorID = colorID;
    }
    public void ChangePlayer2Color(int colorID)
    {
        Player2ColorID = colorID;
    }
}
