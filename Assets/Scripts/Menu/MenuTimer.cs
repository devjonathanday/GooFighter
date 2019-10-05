using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuTimer : MonoBehaviour
{
    public CustomToggleGroup player1, player2;
    public float timer;
    public TextMeshProUGUI timerNumber;
    public GameObject display;
    public GameManager GM;

    private void Start()
    {
        GM = GameManager.FindManager();
    }
    void Update()
    {
        if (player1.ready && player2.ready)
        {
            display.SetActive(true);
            timer -= Time.deltaTime;
            timerNumber.text = ((int)timer).ToString();
        }
        else
        {
            display.SetActive(false);
            timer = 5.99f;
        }
        if(timer < 1)
        {
            GM.SetGameState(GAMESTATE.Rounds);
        }
    }
}