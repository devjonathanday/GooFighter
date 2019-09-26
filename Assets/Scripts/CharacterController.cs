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
        Head.AddComponent<Rigidbody>();
    }
    public void Update(Vector3 _HeadUpV)
    {
        HeadUp(_HeadUpV);
    }
    void HeadUp(Vector3 _HeadUpV)
    {
        Head.GetComponent<Rigidbody>().AddForce(_HeadUpV);
    }
}

public class CharacterController : MonoBehaviour
{
    //Player One and Player Two
    [SerializeField]
    public Player PlayerOne;
    [SerializeField]
    public Player PlayerTwo;

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
        PlayerOne.Update(new Vector3(0, 1, 0));
        PlayerTwo.Update(new Vector3(0, 1, 0));
    }

    //Initilize the players with components
    void InitPlayers()
    {
        PlayerOne.Init();
        PlayerTwo.Init();
    }

    //Movement of Object
    void MovementUpdate(Vector3 _Direction, GameObject _Object)
    {
        //Check if Object has Rigidbody
        Rigidbody RB = _Object.GetComponent<Rigidbody>();
        //Apply rigidbody if Object is missing one
        if (RB == null) RB = _Object.AddComponent<Rigidbody>();
        //Apply Velocity to object
        RB.AddForce(_Direction);
    }
    //Movement of Character
    void MovementUpdate(Player _Player)
    {
        //Move the bottom half, while keeping the top half ragdolly
        //
        //Apply Velocity to object
        _Player.RB.AddForce(_Player.Velocity);
    }
}