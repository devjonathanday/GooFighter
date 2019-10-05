using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    public MeshRenderer myRenderer;
    public ParticleSystem particles;
    public enum MENUTYPE { Start, Quit }
    public MENUTYPE menuType;
    
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 11)
        {
            myRenderer.enabled = false;
        }
    }
}