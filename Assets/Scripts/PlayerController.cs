using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    GameManager Manager;

    public int IndexNumber;

    //Attributes of each character
    int HealthVar = 100;//Health
    int Health { get { return HealthVar; } set { HealthVar = value; if (HealthVar <= 0) { Manager.SetGameState(GAMESTATE.EndOfRound, IndexNumber); GroundCheckDistance = 0; } } }//Init health to maxHealth



    [Header("Basic Attributes")]
    public GameObject Spine;
    public Vector3 Velocity;
    public LayerMask GroundMask;
    public float MoveSpeed;
    public float RotSpeed;
    public float GroundCheckDistance;

    [Header("Bones")]
    public GameObject Head;
    public GameObject Center;
    public GameObject RightArm;
    public GameObject LeftArm;
    [Space(10)]
    public Rigidbody CenterRB;
    public Rigidbody HeadRB;

    bool OnGround;
    public float RotationLerp;

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
        RaycastHit hit;
        if (Physics.Raycast(Spine.transform.position, -Vector3.up, out hit, GroundCheckDistance, GroundMask))
        {
            Debug.DrawRay(Spine.transform.position, -Vector3.up * GroundCheckDistance, Color.red);
            BodyUp(_BodyUpV, _HeadUpV);
        }
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

    void Rotate(Quaternion rotateDir)
    {
        //CenterRB.MoveRotation(Object.transform.rotation * rotateDir);
    }
    void InputController()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(x, 0, y).normalized;

        //CenterRB.AddTorque(CenterRB.transform.up * (input - CenterRB.transform.forward).magnitude * RotSpeed, ForceMode.Impulse);
        float desiredRotation = Mathf.Atan2(input.y, input.x);
        float currentRotation = Mathf.Atan2(CenterRB.transform.forward.z, CenterRB.transform.forward.x);
        //CenterRB.AddTorque(CenterRB.transform.up * (desiredRotation - currentRotation) * RotSpeed, ForceMode.Impulse);
        //Debug.Log(CenterRB.transform.forward + ", " + input);
        Vector3 forwardPos = CenterRB.position + CenterRB.transform.forward;
        Vector3 positionToInput = CenterRB.position + input;

        if (input.sqrMagnitude != 0)
        {
            CenterRB.AddForceAtPosition(positionToInput - forwardPos, forwardPos, ForceMode.Impulse);
        }

        Move(input * MoveSpeed);
        //RotationCenter.rotation = Quaternion.Lerp(RotationCenter.transform.rotation, Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(y, x), Vector3.up), RotationLerp);

    }

    public void DamagePlayer(int _Damage)
    {
        Health -= _Damage;
    }
    public int GetHealth()
    {
        return Health;
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
        if (PlayerTwo.Spine != null) PlayerTwo.Init();
    }

    void FixedUpdate()
    {
        //Update both players
        PlayerOne.Update(BodyForce, HeadForce);
        if (PlayerTwo.Spine != null) PlayerTwo.Update(BodyForce, HeadForce);
    }
}