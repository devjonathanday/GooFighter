using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [System.Serializable]
    public class PlayerHats
    {
        public int PlayerIndex;
        public GameObject WinnerHat;
        public GameObject LoserHat;

        public void DecideHat(int? _LastWinner)
        {
            WinnerHat.SetActive(_LastWinner == PlayerIndex && _LastWinner != null);
            LoserHat.SetActive(_LastWinner != PlayerIndex && _LastWinner != null);
        }
    }
    public PlayerHats Player1Hats;
    public PlayerHats Player2Hats;

    GameManager Manager;

    void Start()
    {
        Manager = GameManager.FindManager();
        Player1Hats.DecideHat(Manager.LastWinner);
        Player2Hats.DecideHat(Manager.LastWinner);
    }
}
