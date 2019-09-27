using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    GameManager Manager;

    //Attributes of each character
    int HealthVar;//Health
    int Health { get { return HealthVar; } set { HealthVar = value; if (HealthVar <= 0) { Manager.SetGameState(GAMESTATE.EndOfRound); } } }//Init health to maxHealth



    [Header("Basic Attributes")]
    public GameObject Object;
    public Vector3 Velocity;
    public float moveSpeed;

    [Header("Bones")]
    public GameObject Head;
    public GameObject Center;
    public GameObject RightArm;
    public GameObject LeftArm;

    Rigidbody CenterRB;
    Rigidbody HeadRB;

    public void Init()
    {
        //Setup a reference to the Rigidbodys
        HeadRB = Head.GetComponent<Rigidbody>();
        CenterRB = Center.GetComponent<Rigidbody>();

        //Setup GameManager
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Update(Vector3 _BodyUpV, Vector3 _HeadUpV)
    {
        BodyUp(_BodyUpV, _HeadUpV);
        InputController();
    }
    void BodyUp(Vector3 _BodyUpV, Vector3 _HeadUpV)
    {
        HeadRB.AddForce(_HeadUpV, ForceMode.Impulse);
        CenterRB.AddForce(_BodyUpV, ForceMode.Impulse);
    }

    void Move(Vector3 moveDir)
    {
        CenterRB.AddForce(moveDir, ForceMode.Acceleration);
    }
    void InputController()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move(new Vector3(x, 0, y) * moveSpeed);
    }
}

public class PlayerController : MonoBehaviour
{
    //Player One and Player Two
    [SerializeField]
    public Player PlayerOne;
    [SerializeField]
    public Player PlayerTwo;

    public Vector3 HeadForce, BodyForce;

    //Players Controls
    //Second Player needs different controls

    void Start()
    {
        PlayerOne.Init();
    }

    void Update()
    {
        //Update both players
        PlayerOne.Update(BodyForce, HeadForce);
        //PlayerTwo.Update(new Vector3(0, 1, 0));
    }
}