using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public CustomToggleGroup toggleMenu;
    public Material[] colors;
    [Space(10)]
    public int playerID;
    public SkinnedMeshRenderer meshRenderer;
    public Rigidbody headRB, centerRB;
    public float headForce, centerForce;
    
    void Update()
    {
        headRB.AddForce(Vector3.up * headForce);
        centerRB.AddForce(Vector3.up * centerForce);
    }

    public void ChangeColor(int ID)
    {
        meshRenderer.material = colors[ID];
    }
}