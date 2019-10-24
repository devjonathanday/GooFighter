using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class JellyRenderTarget : MonoBehaviour
{
    RenderTexture renderTexArray;
    Texture2D rgbTex;
    Color32[] rawRgb;
    Material myMat;

    MeshFilter meshFilter;
    Mesh mesh;
    
    Vector3[] currentMeshVerts;

    int latestTex = 0;

    void Awake()
    {
        RenderTextureDescriptor d = new RenderTextureDescriptor(1024, 1024, RenderTextureFormat.ARGBFloat);
        d.dimension = TextureDimension.Tex2DArray;
        d.volumeDepth = 2;

        renderTexArray = new RenderTexture(d);
        renderTexArray.Create();
        renderTexArray.name = "Jelly Render Texture Array";
        rgbTex = new Texture2D(1024, 1024, TextureFormat.RGBAFloat, false);

        myMat = GetComponent<Renderer>().material;

        myMat.SetInt("_DepthSlice", latestTex);

        Graphics.SetRenderTarget(renderTexArray.colorBuffer, renderTexArray.depthBuffer, 0, CubemapFace.Unknown, latestTex);

        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        GetVertices();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyPressure();
        }
    }

    private void OnDestroy()
    {
        renderTexArray.Release();
    }

    private void GetVertices()
    {
        currentMeshVerts = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            currentMeshVerts[i] = mesh.vertices[i];
        }
    }

    public void ApplyPressure()
    {
        RenderTexture.active = renderTexArray;
        rgbTex.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
        rgbTex.Apply();
        RenderTexture.active = null;

        rawRgb = rgbTex.GetPixels32();

        for (int i = 0; i < currentMeshVerts.Length; i++)
        {
            currentMeshVerts[i] = new Vector3(rawRgb[i].r, rawRgb[i].g, rawRgb[i].b);
        }
        
    }
}
