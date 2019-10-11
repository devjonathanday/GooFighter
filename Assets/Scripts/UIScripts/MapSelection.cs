using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    GameManager Manager;
    public MAPS CurrentlySelectedMap;


    private void Awake()
    {
        Manager = GameManager.FindManager();//Best static function EVER!!!!!!!
    }

    public void ButtonSelected(string _Name)
    {
        //Parses the string and finds which Number it is in the MAPS Enum
        CurrentlySelectedMap = (MAPS)System.Enum.Parse(typeof(MAPS), _Name); 
    }
    public void Selected()
    {
        Manager.CurrentMap = CurrentlySelectedMap;
    }
}
