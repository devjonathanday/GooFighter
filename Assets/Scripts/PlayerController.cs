using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [Header("Basic Attributes")]
    public GameObject Object;
    public Vector3 Velocity;
    public Rigidbody RB;

    [Header("Bones")]
    public GameObject Head;
    public GameObject Center;
    public GameObject RightArm;
    public GameObject LeftArm;

    public void Init()
    {
        //Check RigidBody of Player
        if (RB == null)
        {
            //If non existant, attempt to assign
            if ((RB = Object.GetComponent<Rigidbody>()) == null)
            {
                //If fail again, add a rigidbody to player
                RB = Object.AddComponent<Rigidbody>();
            }
        }
    }
    public void Update(Vector3 _BodyUpV, Vector3 _HeadUpV)
    {
        BodyUp(_BodyUpV, _HeadUpV);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move(new Vector3(x, 0, y));
    }
    void BodyUp(Vector3 _BodyUpV, Vector3 _HeadUpV)
    {
        Head.GetComponent<Rigidbody>().AddForce(_HeadUpV, ForceMode.Impulse);
        Center.GetComponent<Rigidbody>().AddForce(_BodyUpV, ForceMode.Impulse);
    }

    void Move(Vector3 moveDir)
    {
        RB.AddForce(moveDir, ForceMode.Acceleration);
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
        //Initilize both players
        InitPlayers();
    }

    void Update()
    {
        //Update both players
        PlayerOne.Update(BodyForce, HeadForce);
        //PlayerTwo.Update(new Vector3(0, 1, 0));
    }

    //Initilize the players with components
    void InitPlayers()
    {
        PlayerOne.Init();
        //PlayerTwo.Init();
    }
}