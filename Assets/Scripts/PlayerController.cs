using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    GameManager Manager;

    public int IndexNumber;
    public int EnemyIndexNumber;

    //Attributes of each character
    int HealthVar = 100;//Health
    int PreviousHealthVar = 100;
    int Health
    {
        get { return HealthVar; } //Init health to maxHealth

        set
        {
            HealthVar = value;
            if (HealthVar <= 0)
            {
                Manager.SetGameState(GAMESTATE.EndOfRound, EnemyIndexNumber);
                GroundCheckDistance = 0;
                HealthVar = 0;
            }
        }
    }
    int PreviousHealth
    {
        get { return PreviousHealthVar; }
        set { PreviousHealthVar = value; }
    }
    
    [Header("Basic Attributes")]
    public GameObject Spine;
    public Vector3 Velocity;
    public LayerMask GroundMask;
    public float MoveSpeed;
    public float RotSpeed;
    public float GroundCheckDistance;
    public AudioSource walkSplats;

    [Header("Bones")]
    public GameObject Head;
    public GameObject Center;
    public GameObject RightArm;
    public GameObject LeftArm;
    [Space(10)]
    public Rigidbody CenterRB;
    public Rigidbody HeadRB;

    bool OnGround;

    public void Init()
    {
        //Setup a reference to the Rigidbodys
        HeadRB = Head.GetComponent<Rigidbody>();
        CenterRB = Center.GetComponent<Rigidbody>();

        //Setup GameManager
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Update(Vector3 _BodyUpV, Vector3 _HeadUpV, float _Drag, int playerID)
    {
        if (GetHealth() > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(Spine.transform.position, -Vector3.up, out hit, GroundCheckDistance, GroundMask))
            {
                OnGround = true;
                Debug.DrawRay(Spine.transform.position, -Vector3.up * GroundCheckDistance, Color.red);
                BodyUp(_BodyUpV, _HeadUpV);
            }
            else
            {
                OnGround = false;
            }

            if (OnGround)
            {
                CenterRB.velocity *= _Drag;
                CenterRB.angularVelocity *= _Drag;
            }

            InputController(playerID);
        }
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
    void InputController(int playerID)
    {
        float x = 0;
        float y = 0;

        x = Manager.GetAxis(playerID, "Horizontal");
        y = Manager.GetAxis(playerID, "Vertical");

        Vector3 input = new Vector3(x, 0, y).normalized;

        float desiredRotation = Mathf.Atan2(input.y, input.x);
        float currentRotation = Mathf.Atan2(CenterRB.transform.forward.z, CenterRB.transform.forward.x);
        Vector3 forwardPos = CenterRB.position + CenterRB.transform.forward;
        Vector3 positionToInput = CenterRB.position + input;

        if (input.sqrMagnitude != 0)
        {
            CenterRB.AddForceAtPosition((positionToInput - forwardPos) * RotSpeed, forwardPos, ForceMode.Impulse);
            walkSplats.volume = Mathf.Lerp(walkSplats.volume, 0.25f, 0.1f);
        }
        else walkSplats.volume = Mathf.Lerp(walkSplats.volume, 0, 0.1f);

        Move(input * MoveSpeed);
    }

    public void DamagePlayer(int _Damage)
    {
        PreviousHealth = Health;
        Health -= _Damage;
    }
    public int GetHealth()
    {
        return Health;
    }
    public int GetPreviousHealth()
    {
        return PreviousHealth;
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
    public float Drag;

    public SplatParticle SplatParticleController;

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
        PlayerOne.Update(BodyForce, HeadForce, Drag, 1);
        if (PlayerTwo.Spine != null) PlayerTwo.Update(BodyForce, HeadForce, Drag, 2);
    }
}