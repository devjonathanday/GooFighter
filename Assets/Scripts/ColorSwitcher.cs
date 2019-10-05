using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public MeshRenderer[] Player1UI;
    public SkinnedMeshRenderer Player1Model;

    public MeshRenderer[] Player2UI;
    public SkinnedMeshRenderer Player2Model;

    public Material[] playerColors;
    public Material[] UIColors;

    void Start()
    {
        GameManager GM = GameManager.FindManager();

        Player1Model.material = playerColors[GM.Player1ColorID];
        Player2Model.material = playerColors[GM.Player2ColorID];

        for (int i = 0; i < Player1UI.Length; i++)
            Player1UI[i].material = UIColors[GM.Player1ColorID];

        for (int i = 0; i < Player2UI.Length; i++)
            Player2UI[i].material = UIColors[GM.Player2ColorID];
    }
}