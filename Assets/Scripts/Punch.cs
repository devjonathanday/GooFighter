using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    Rigidbody RB;
    public GameObject Enemy;
    public PlayerController PController;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RB.AddForce(30 * (Enemy.transform.position - transform.position).normalized,ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (RB.velocity.magnitude > 5)
        {
            if(collision.gameObject.transform.root.gameObject.name == Enemy.name)
            {
                PController.PlayerOne.DamagePlayer(((int)RB.velocity.magnitude));
            }
        }
    }
}
