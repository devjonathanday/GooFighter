using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControllerSupport : MonoBehaviour
{
    int menuStates = 1;
    int VertmenuState = 0;
    int MenuStates
    {
        set
        {
            if (value == 1 && menuStates != 1)
            {
                PlayButton.Jiggle();
                ScaleTimer = Time.time;
                ResetStartPos();
            }
            else if (value == 0 && menuStates != 0)
            {
                QuitButton.Jiggle();
                ScaleTimer = Time.time;
                ResetStartPos();
            }
            menuStates = (Mathf.Clamp(value,0,1));
            
        }
        get
        {
            return menuStates;
        }
    }
    int VertMenuStates
    {
        set
        {
            if (value == 1 && VertmenuState != 1)
            {
                ControlsButton.Jiggle();
                ScaleTimer = Time.time;
                ResetStartPos();
            }
            if (value == 0 && VertmenuState != 0)
            {
                if (menuStates == 1)
                {
                    PlayButton.Jiggle();
                    ScaleTimer = Time.time;
                    ResetStartPos();
                }
                else if (menuStates == 0)
                {
                    QuitButton.Jiggle();
                    ScaleTimer = Time.time;
                    ResetStartPos();
                }
            }
            VertmenuState = (Mathf.Clamp(value, 0, 1));

        }
        get
        {
            return VertmenuState;
        }
    }

    public Jellyfier Title;

    public Jellyfier PlayButton;
    public Jellyfier QuitButton;
    public Jellyfier ControlsButton;

    public int MaxEnlargeSize;
    public int MinEnSmallSize;

    GameManager Manager;

    float ScaleTimer;

    public float SpeedMultiplier = 2;


    Vector3 PlayStartPos;
    Vector3 QuitStartPos;
    Vector3 ControlsStartPos;

    private void Start()
    {
        Manager = GameManager.FindManager();
    }

    private void Update()
    {
        if (Manager.GetAxis(1, "Horizontal") * 2 + Manager.GetAxis(2, "Horizontal") < 0 && VertMenuStates == 0) UpdateMenuState(1);
        if (Manager.GetAxis(1, "Horizontal") * 2 + Manager.GetAxis(2, "Horizontal") > 0 && VertMenuStates == 0) UpdateMenuState(-1);

        if (Manager.GetAxis(1, "Vertical") * 2 + Manager.GetAxis(2, "Vertical") < 0) UpdateVertMenuState(1);
        if (Manager.GetAxis(1, "Vertical") * 2 + Manager.GetAxis(2, "Vertical") > 0) UpdateVertMenuState(-1);

        float Timer = Mathf.Clamp(Time.time - ScaleTimer, 0, 1);

        if (MenuStates == 1 && VertmenuState == 0)
        {
            PlayButton.transform.localScale = Vector3.Lerp(PlayStartPos, new Vector3(MaxEnlargeSize,MaxEnlargeSize,MaxEnlargeSize), Timer * SpeedMultiplier);
            QuitButton.transform.localScale = Vector3.Lerp(QuitStartPos, new Vector3(MinEnSmallSize,MinEnSmallSize,MinEnSmallSize), Timer * SpeedMultiplier);
            ControlsButton.transform.localScale = Vector3.Lerp(ControlsStartPos, new Vector3(MinEnSmallSize,MinEnSmallSize,MinEnSmallSize), Timer * SpeedMultiplier);
        }
        else if(MenuStates == 0 && VertmenuState == 0)
        {
            QuitButton.transform.localScale = Vector3.Lerp(QuitStartPos, new Vector3(MaxEnlargeSize, MaxEnlargeSize, MaxEnlargeSize), Timer * SpeedMultiplier);
            PlayButton.transform.localScale = Vector3.Lerp(PlayStartPos, new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), Timer * SpeedMultiplier);
            ControlsButton.transform.localScale = Vector3.Lerp(ControlsStartPos, new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), Timer * SpeedMultiplier);
        }
        else if(VertmenuState == 1)
        {
            ControlsButton.transform.localScale = Vector3.Lerp(ControlsStartPos, new Vector3(MaxEnlargeSize, MaxEnlargeSize, MaxEnlargeSize), Timer * SpeedMultiplier);
            PlayButton.transform.localScale = Vector3.Lerp(PlayStartPos, new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), Timer * SpeedMultiplier);
            QuitButton.transform.localScale = Vector3.Lerp(QuitStartPos, new Vector3(MinEnSmallSize, MinEnSmallSize, MinEnSmallSize), Timer * SpeedMultiplier);
        }

        if(Manager.GetButtonDown(1, "Submit") || Manager.GetButtonDown(2, "Submit"))
        {
            if (MenuStates == 1 && VertmenuState == 0) Manager.SetGameState(GAMESTATE.MapSelect);
            if (VertmenuState == 1) Manager.SetGameState(GAMESTATE.Controls);
            else Manager.Quit();
        }
        Title.Jiggle();
    }

    void UpdateMenuState(int _Amount)
    {
        MenuStates += _Amount;
    }
    void UpdateVertMenuState(int _Amount)
    {
        VertMenuStates += _Amount;
    }
    public void GrowBigPlay()
    {
        MenuStates = 1;
        VertMenuStates = 0;
    }
    public void GrowBigQuit()
    {
        MenuStates = 0;
        VertMenuStates = 0;
    }
    public void GrowBigControls()
    {
        VertMenuStates = 1;
    }
    void ResetStartPos()
    {
        PlayStartPos = PlayButton.transform.localScale;
        QuitStartPos = QuitButton.transform.localScale;
        ControlsStartPos = ControlsButton.transform.localScale;
    }
}
