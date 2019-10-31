using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControllerSupport : MonoBehaviour
{
    int menuStates = 1;
    int MenuStates
    {
        set
        {
            if (value == 1 && menuStates != 1)
            {
                PlayButton.Jiggle();
                ScaleTimer = Time.time;
            }
            else if (value == 0 && menuStates != 0)
            {
                QuitButton.Jiggle();
                ScaleTimer = Time.time;
            }
            menuStates = (Mathf.Clamp(value,0,1));
            
        }
        get
        {
            return menuStates;
        }
    }

    public Jellyfier Title;

    public Jellyfier PlayButton;
    public Jellyfier QuitButton;

    public int MaxEnlargeSize;
    public int MinEnSmallSize;

    GameManager Manager;

    float ScaleTimer;

    public float SpeedMultiplier = 2;

    private void Start()
    {
        Manager = GameManager.FindManager();
    }

    private void Update()
    {
        if (Manager.GetAxis(1, "Horizontal") + Manager.GetAxis(2, "Horizontal") < 0) UpdateMenuState(1);
        if (Manager.GetAxis(1, "Horizontal") + Manager.GetAxis(2, "Horizontal") > 0) UpdateMenuState(-1);

        float Timer = Mathf.Clamp(Time.time - ScaleTimer, 0, 1);

        if(MenuStates == 1)
        {
            PlayButton.transform.localScale = Vector3.Lerp(new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), new Vector3(MaxEnlargeSize,MaxEnlargeSize,MaxEnlargeSize), Timer * SpeedMultiplier);
            QuitButton.transform.localScale = Vector3.Lerp(new Vector3(MaxEnlargeSize,MaxEnlargeSize,MaxEnlargeSize), new Vector3(MinEnSmallSize,MinEnSmallSize,MinEnSmallSize), Timer * SpeedMultiplier);
        }
        else
        {
            QuitButton.transform.localScale = Vector3.Lerp(new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), new Vector3(MaxEnlargeSize, MaxEnlargeSize, MaxEnlargeSize), Timer * SpeedMultiplier);
            PlayButton.transform.localScale = Vector3.Lerp(new Vector3(MaxEnlargeSize, MaxEnlargeSize, MaxEnlargeSize), new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), Timer * SpeedMultiplier);
        }

        if(Manager.GetButtonDown(1, "Submit") || Manager.GetButtonDown(2, "Submit"))
        {
            if (MenuStates == 1) Manager.SetGameState(GAMESTATE.MapSelect);
            else Manager.Quit();
        }
        Title.Jiggle();
    }

    void UpdateMenuState(int _Amount)
    {
        MenuStates += _Amount;
    }




    public void GrowBigPlay()
    {
        MenuStates = 1;
    }
    public void GrowBigQuit()
    {
        MenuStates = 0;
    }

}
