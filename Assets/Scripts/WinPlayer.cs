using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinPlayer : MonoBehaviour
{
    public Material[] colors;
    public bool winner;
    public SkinnedMeshRenderer meshRenderer;
    public Rigidbody headRB, centerRB, leftHandRB, rightHandRB;
    public float headForce, centerForce, handForce;
    public GameManager GM;
    public TextMeshProUGUI congratsText;
    public GameObject readySymbol;
    public bool ready;
    public int playerNum;
    public WinPlayer losingPlayer;

    void Start()
    {
        GM = GameManager.FindManager();
        if (GM.Player1Score >= 2)
        {
            if (winner)
            {
                playerNum = 1;
                ChangeColor(GM.Player1ColorID);
                congratsText.color = colors[GM.Player1ColorID].color;
            }
            else
            {
                playerNum = 2;
                ChangeColor(GM.Player2ColorID);
            }
        }
        else if (GM.Player2Score >= 2)
        {
            if (winner)
            {
                playerNum = 2;
                ChangeColor(GM.Player2ColorID);
                congratsText.color = colors[GM.Player2ColorID].color;
            }
            else
            {
                playerNum = 1;
                ChangeColor(GM.Player1ColorID);
            }
        }
    }

    void Update()
    {
        headRB.AddForce(Vector3.up * headForce);
        centerRB.AddForce(Vector3.up * centerForce);
        leftHandRB.AddForce(Vector3.up * handForce);
        rightHandRB.AddForce(Vector3.up * handForce);

        if (playerNum == 1 && GM.GetButtonDown(1, "Submit") ||
            playerNum == 2 && GM.GetButtonDown(2, "Submit"))
        {
            readySymbol.SetActive(false);
            ready = true;
        }
        if ((winner) && losingPlayer.ready && ready) GM.ResetFromWinScreen();
    }

    public void ChangeColor(int ID)
    {
        meshRenderer.material = colors[ID];
    }
}