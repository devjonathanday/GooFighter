using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GAMESTATE { MainMenu, Rounds, EndOfRound}

public class GameManager : MonoBehaviour
{
    static GameManager Manager;//Singletoness

    GAMESTATE CurrentState;
    int Round;


    void Start()
    {
        //singleton
        if (Manager == null) Manager = this;
        else Destroy(gameObject);

        //GameManager Don't destroy from scene to scene
        DontDestroyOnLoad(this);
    }

    public void NextRound()
    {
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

    public void SetGameState(GAMESTATE _NewState)
    {
        CurrentState = _NewState;
    }
    public GAMESTATE GetState()
    {
        return CurrentState;
    }
}
