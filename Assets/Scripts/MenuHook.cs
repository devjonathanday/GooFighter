using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHook : MonoBehaviour
{
    public GameManager GM;

    void Start()
    {
        GM = GameManager.FindManager();
    }

    public void SetGameState(string newState)
    {
        GM.SetGameState(newState);
    }

    public void Quit()
    {
        GM.Quit();
    }
}