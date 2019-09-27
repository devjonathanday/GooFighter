using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [Header("GameManager")]
    public GameManager GameManager;

    [Header("UIDisplays")]
    public TextMeshProUGUI RoundDisplay;

    void Update()
    {
        DisplayRoundNumber();
    }

    void DisplayRoundNumber()
    {
        //Make the text equal the current round
        RoundDisplay.text = GameManager.GetRound().ToString();
        CheatRound();
    }
    void CheatRound()
    {
        //Move to the next round when P is pressed
        if (Input.GetKeyDown(KeyCode.P)) GameManager.NextRound();
    }
}
