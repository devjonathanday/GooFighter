using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    Rigidbody RB;

    GameManager Manager;

    int IdxNumber;

    public GameObject Enemy;
    public GameObject Center;
    public PlayerController PController;

    public float PunchForce = 3.0f;
    public float MinimumForceForDamage = 0.5f;
    public float damageScalar;

    public string punchInput;

    public float launchSpeed;

    public AudioSource punchAudio;
    public AudioClip[] punchSounds;
    
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Manager = GameManager.FindManager();

        IdxNumber = (transform.root.name == "Player1") ? 1:2;
    }
    void Update()
    {
        ThrowPunch();
    }

    void ThrowPunch()
    {
        //Lowered mass of hands to stop the player from moving too much when punching
        //Direction the player will be throwing the punch
        //Calculat which direction the player's Center is facing
        Vector3 PunchDirection = Center.transform.forward;//OLD:: (Enemy.transform.position - transform.position);
        //Add a force in the direction of the puch direction
        if (/*Input.GetButtonDown(punchInput) ||*/ Manager.GetButtonDown(IdxNumber, punchInput))
            RB.AddForce(PunchForce * PunchDirection.normalized, ForceMode.Impulse);
    }

    float HighestVelocityFromContacts(ContactPoint[] _Contacts)
    {
        //Return the Greatest velocity point
        return HighestVelVecFromContacts(_Contacts).magnitude;
    }

    Vector3 HighestVelVecFromContacts(ContactPoint[] _Contacts)
    {
        //Init Velocity
        Vector3 ReturningValue = Vector3.zero;

        //Itterate through all contact points
        for (int i = 0; i < _Contacts.Length; i++)
        {
            //Get the current contact points velocity
            Vector3 TempVelocity = RB.GetPointVelocity(_Contacts[i].point);
            //If the current velocity is greator than the current greatest
            if (TempVelocity.magnitude > ReturningValue.magnitude)
            {
                //Set the new greatest to the current
                ReturningValue = TempVelocity;
            }
        }
        //Return the Greatest velocity point
        return ReturningValue;
    }

    void OnCollisionEnter(Collision collision)
    {
        //If the object collided with does not apply to own body
        if (collision.transform.root.gameObject.name != transform.root.gameObject.name)
        {
            //Checking to see how much force the player is hitting the enemy with
            if (RB.velocity.magnitude > MinimumForceForDamage)
                //Making sure the enemy is the one being hit
                if (Enemy != null)
                {
                    if (collision.transform.root.gameObject.name == Enemy.transform.root.gameObject.name)
                    {
                        ContactPoint[] Contact = new ContactPoint[collision.contactCount];
                        collision.GetContacts(Contact);
                        //print(HighestVelocityFromContacts(Contact));

                        //Check for both players healths are greator than zero
                        if (PController.PlayerOne.GetHealth() > 0 && PController.PlayerTwo.GetHealth() > 0)
                            //Checking if the enemy is Player One
                            if (collision.transform.root.gameObject.name == PController.PlayerOne.Center.transform.root.gameObject.name)
                            {
                                //Apply damage to player one
                                PController.PlayerOne.DamagePlayer((int)(HighestVelocityFromContacts(Contact) * damageScalar));
                                //Particle Effect
                                PController.SplatParticleController.DisplayHitParticle(HighestVelVecFromContacts(Contact) / 2, Manager.playerColors[Manager.Player1ColorID].color, Contact[0].point);
                                //Play random sound from array
                                AudioController.PlayRandomSound(punchAudio, punchSounds);
                                punchAudio.volume = Mathf.Max((HighestVelocityFromContacts(Contact) * damageScalar) / 10.0f, 0.25f);
                            }
                            //Or Enemy is Player Two
                            else if (collision.transform.root.gameObject.name == PController.PlayerTwo.Center.transform.root.gameObject.name)
                            {
                                //Apply damage to player two
                                PController.PlayerTwo.DamagePlayer((int)(HighestVelocityFromContacts(Contact) * damageScalar));
                                //Particle Effect
                                PController.SplatParticleController.DisplayHitParticle(HighestVelVecFromContacts(Contact) / 2, Manager.playerColors[Manager.Player2ColorID].color, Contact[0].point);
                                //Play random sound from array
                                AudioController.PlayRandomSound(punchAudio, punchSounds);
                                punchAudio.volume = Mathf.Max((HighestVelocityFromContacts(Contact) * damageScalar) / 10.0f, 0.25f);
                            }
                        Manager.JiggleEverything();
                    }
                }

            //Rigidbody of the Object that was collided with
            Rigidbody HitPointRB = collision.gameObject.GetComponent<Rigidbody>();

            //If the part colliding with has an Rigidbody
            if (HitPointRB != null)
            {
                //Adds the force the arm was traveling
                HitPointRB.AddForce(RB.velocity * launchSpeed, ForceMode.Impulse);
                //print("Pushed " + collision.gameObject.name + " with " + RB.velocity + " force");
            }
        }
    }
}
