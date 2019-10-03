using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    Rigidbody RB;

    public GameObject Enemy;
    public GameObject Center;
    public PlayerController PController;

    public string punchInput;

    public float PunchForce = 3.0f;
    public float MinimumForceForDamage = 0.5f;
    public float damageScalar;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
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
        if (/*Input.GetButtonDown(punchInput) ||*/ Input.GetButtonDown(punchInput))
            RB.AddForce(PunchForce * PunchDirection.normalized, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        //If the object collided with does not apply to own body
        if (collision.transform.root.gameObject.name != transform.root.gameObject.name)
        {
            //Checking to see how much force the player is hitting the enemy with
            if (RB.velocity.magnitude > MinimumForceForDamage)
                //Making sure the enemy is the one being hit
                if (collision.transform.root.gameObject.name == Enemy.transform.root.gameObject.name)
                    //Check for both players healths are greator than zero
                    if (PController.PlayerOne.GetHealth() > 0 && PController.PlayerTwo.GetHealth() > 0)
                    {
                        //Checking if the enemy is Player One
                        if (collision.transform.root.gameObject.name == PController.PlayerOne.Center.transform.root.gameObject.name)
                            //Apply damage to player one
                            PController.PlayerOne.DamagePlayer((int)(RB.velocity.magnitude * damageScalar));
                        //Or Enemy is Player Two
                        else if (collision.transform.root.gameObject.name == PController.PlayerTwo.Center.transform.root.gameObject.name)
                            //Apply damage to player two
                            PController.PlayerTwo.DamagePlayer((int)(RB.velocity.magnitude * damageScalar));
                    }
            //Rigidbody of the Object that was collided with
            Rigidbody HitPointRB = collision.gameObject.GetComponent<Rigidbody>();

            //If the part colliding with has an Rigidbody
            if (HitPointRB != null)
            {
                //Adds the force the arm was traveling
                HitPointRB.AddForce(RB.velocity * 2, ForceMode.Impulse);
                //print("Pushed " + collision.gameObject.name + " with " + RB.velocity + " force");
            }
        }
    }
}
