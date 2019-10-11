using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyRenderTarget : MonoBehaviour
{
    Texture2D[] jellyRendTexture = new Texture2D[2];
    Material myMat;
    [SerializeField]
    Shader shader;

    void Awake()
    {
        jellyRendTexture[0] = new Texture2D(1024, 1024, TextureFormat.RGBAFloat, false);
        jellyRendTexture[1] = new Texture2D(1024, 1024, TextureFormat.RGBAFloat, false);

        myMat = new Material(shader);
    }

    void Update()
    {
        
    }
}
