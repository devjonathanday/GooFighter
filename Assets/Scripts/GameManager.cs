using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public enum GAMESTATE { MainMenu, MapSelect, ColorSelect, Rounds, EndOfRound, TransitionRound, WinScreen}
public enum MAPS { Playground, PoolTable, Ring}

public class GameManager : MonoBehaviour
{
    static GameManager Manager;//Singletoness

    public List<Jellyfier> jellyObjects;

    public MAPS CurrentMap;//Current map to be loaded

    //[HideInInspector]
    public GAMESTATE CurrentState;//Current state of the game
    int Round = 0;//Which round is current
    [HideInInspector]
    public bool HasCheckedRoundNumber = false;//Checks to see if the 

    public int Player1Score = 0;//Score For Player One
    public int Player2Score = 0;//Score For Player Two

    public int Player1ColorID = 0;
    public int Player2ColorID = 0;

    public Material[] playerColors;

    public int? LastWinner = null;

    public bool joystick;

    //0 = Red
    //1 = Yellow
    //2 = Green
    //3 = Blue
    //4 = Pink
    //5 = Black

    public Rewired.Player Player1RW;
    public Rewired.Player Player2RW;


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

        gameObject.name = "GameManager";


        //Rewired Player Setup
        Player1RW = ReInput.players.GetPlayer(0);
        Player2RW = ReInput.players.GetPlayer(1);

    }

    void Update()
    {
        TimerUpdate();
        CHEAT();
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

        if (CurrentState == GAMESTATE.Rounds)
        {
            //Go to Rounds
            SceneManager.LoadScene(CurrentMap.ToString());
        }

        //If the Current state is the end of round
        if (CurrentState == GAMESTATE.TransitionRound)
        {
            CheckForWinner();
            //NextRound
            NextRound();
            //Reset the current round for the next play

            ResetingRound();

            CheckForWinner();
            //Resets the state to be the rounds
            CurrentState = GAMESTATE.Rounds;
        }

        if (CurrentState == GAMESTATE.WinScreen)
        {
            SceneManager.LoadScene("WinScreen");
            return;
        }

        if(CurrentState == GAMESTATE.MapSelect)
        {
            SceneManager.LoadScene("MapSelection");
            return;
        }

    }
    public void SetGameState(GAMESTATE _NewState, int _PlayerNumber)
    {
        CurrentState = _NewState;

        //If the Current state is the end of round
        if (CurrentState == GAMESTATE.EndOfRound)
        {
            LastWinner = _PlayerNumber;
            //Record which player won
            IncreasePlayerScore(_PlayerNumber);
            //Set Round ending timer
            RoundWaitingPeriod();
            //Clear jelly list
            jellyObjects.Clear();
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetGameState(GAMESTATE.EndOfRound, 1);//Player one wins
            RoundWaitingPeriod();
        }
        if (Input.GetKeyDown(KeyCode.M))
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

    public static GameManager FindManager()
    {
        GameManager ReturningManager;

        GameObject TempManager = GameObject.FindGameObjectWithTag("GameManager");
        if (TempManager == null) ReturningManager = CreateMissingManager().GetComponent<GameManager>();
        else ReturningManager = TempManager.GetComponent<GameManager>();

        return ReturningManager;
    }

    public void ChangePlayerColor(int playerID, int colorID)
    {
        if (playerID == 1) Player1ColorID = colorID;
        if (playerID == 2) Player2ColorID = colorID;
    }

    public string GetCurrentMapName()
    {
        //Gets the name out of the enum
        return System.Enum.GetName(typeof(MAPS), CurrentMap).ToString();
    }

    public void CheckForWinner()
    {
        if (Player1Score >= 2 || Player2Score >= 2)
        {
            SetGameState(GAMESTATE.WinScreen);

        }
    }

    public void ResetFromWinScreen()
    {
        Player1Score = 0;
        Player2Score = 0;
        SceneManager.LoadScene("MapSelection");
    }

    public void JiggleEverything()
    {
        for (int i = 0; i < jellyObjects.Count; i++)
        {
            if (jellyObjects[i] != null && jellyObjects[i].gameObject.activeSelf)
                jellyObjects[i].Jiggle();
        }
    }


    //Button related functions
    public bool GetButtonDown(int _PlayerID, string _Button)
    {
        if(_PlayerID == 1)
        {
            return Player1RW.GetButtonDown(_Button);
        }
        else if(_PlayerID == 2)
        {
            return Player2RW.GetButtonDown(_Button);
        }
        return false;
    }
    public bool GetButtonUp(int _PlayerID, string _Button)
    {
        if (_PlayerID == 1)
        {
            return Player1RW.GetButtonUp(_Button);
        }
        else if (_PlayerID == 2)
        {
            return Player2RW.GetButtonUp(_Button);
        }
        return false;
    }
    public bool GetButton(int _PlayerID, string _Button)
    {
        if (_PlayerID == 1)
        {
            return Player1RW.GetButton(_Button);
        }
        else if (_PlayerID == 2)
        {
            return Player2RW.GetButton(_Button);
        }
        return false;
    }
    public float GetAxis(int _PlayerID, string _Axis)
    {
        if (_PlayerID == 1)
        {
            return Player1RW.GetAxis(_Axis);
        }
        else if (_PlayerID == 2)
        {
            return Player2RW.GetAxis(_Axis);
        }
        return 0;
    }
}