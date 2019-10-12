using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelController
{
    public GameObject Level;
    public GameObject Camera;
    [HideInInspector]
    public Animator Anim;

    [HideInInspector]
    public Image MyImage;

    RenderTexture TargetRenderer;

    float Frame;
    public float CurrentFrame
    {
        get { return Frame; }
        set { if (value > 1) value = 0; Frame = value; }
    }

    public void Init()
    {
        TargetRenderer = Camera.GetComponent<Camera>().targetTexture;
        Anim = Camera.GetComponent<Animator>();
        MyImage = Level.GetComponent<Image>();
    }
    public void Update(float _Speed)
    {
        CurrentFrame += Time.deltaTime * _Speed;
        Anim.Play(0, 0, CurrentFrame);
    }
}


public class MapSelection : MonoBehaviour
{
    GameManager Manager;

    public float SpeedOfAnimation;
    public float InputDelay;
    float LastInput;

    [Header("Controls")]
    public string HorizontalAxis;
    public string Submit;
    public string Cancel;

    [Header("Level Objects")]
    public List<LevelController> Levels = new List<LevelController>();
    int Index = 0;

    [Header("Colors")]
    public Color UnSelectedColor;
    public Color SelectedColor;

    [Header("Scene Transitions")]
    public string NextScene;
    public string PreviousScene;

    private bool UpdateCameras = false;
    private void Awake()
    {
        Manager = GameManager.FindManager();//Best static function EVER!!!!!!!
        LastInput = Time.time - InputDelay + .1f;
    }
    private void Start()
    {
        for(int i = 0; i < Levels.Count; i++)
        {
            Levels[i].Init();
        }
    }

    private void Update()
    {
        UpdateUI();
        ButtonUpdate();
        Levels[Index].Update(SpeedOfAnimation);
    }

    void UpdateUI()
    {
        //CHeck to see if enough time has been inbetween inputs
        if (Time.time - LastInput > InputDelay)
        {
            float Horizontal = GetAxis(HorizontalAxis);
            if (Horizontal < 0)
            {
                Index--;
                LastInput = Time.time;
                Index = Mathf.Clamp(Index, 0, Levels.Count - 1);
                SelectCurrent(Index);
            }
            else if (Horizontal > 0)
            {
                Index++;
                LastInput = Time.time;
                Index = Mathf.Clamp(Index, 0, Levels.Count - 1);
                SelectCurrent(Index);
            }
            if (!UpdateCameras)
            {
                UpdateCameras = true;
                SelectCurrent(Index);
            }
        }
    }

    void SelectCurrent(int _CurrentSelected)
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            if (i != Index)
            {
                Levels[i].MyImage.color = UnSelectedColor;
                Levels[i].Anim.enabled = false;
                Levels[i].Camera.SetActive(false);
            }
            else
            {
                Levels[_CurrentSelected].MyImage.color = SelectedColor;
                Levels[i].Anim.enabled = true;
                Levels[i].Camera.SetActive(true);
            }
        }
    }

    void OutOfBoundsCheck(int _Index, int _ListLength)
    {
        if (_Index < 0) _Index = _ListLength - 1;
        else if (_Index > _ListLength - 1) _Index = 0;
    }
    float GetAxis(string _Axis)
    {
        return Input.GetAxisRaw(_Axis);
    }
    bool GetButtonDown(string _Button)
    {
        return Input.GetButtonDown(_Button);
    }

    void ButtonUpdate()
    {
        if(GetButtonDown(Cancel))
        {
            SceneManager.LoadScene(PreviousScene);
        }
        if (GetButtonDown(Submit))
        {
            Manager.CurrentMap = (MAPS)System.Enum.Parse(typeof(MAPS), Levels[Index].Level.name);
            SceneManager.LoadScene(NextScene);
        }
    }
}
