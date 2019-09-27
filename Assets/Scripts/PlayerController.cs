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
    public float moveSpeed;

    [Header("Bones")]
    public GameObject Head;
    public GameObject Center;
    public GameObject RightArm;
    public GameObject LeftArm;

    public void Update(Vector3 _BodyUpV, Vector3 _HeadUpV)
    {
        BodyUp(_BodyUpV, _HeadUpV);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move(new Vector3(x, 0, y) * moveSpeed);
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

    }

    void Update()
    {
        //Update both players
        PlayerOne.Update(BodyForce, HeadForce);
        //PlayerTwo.Update(new Vector3(0, 1, 0));
    }
}