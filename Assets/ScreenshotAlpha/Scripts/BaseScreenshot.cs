using UnityEngine;
using System.IO;
using System;
using UnityEditor;

#if UNITY_EDITOR

public class BaseScreenshot : MonoBehaviour
{
    public Camera Camera;
    public ResolutionType ResolutionType;
    public string[] LayersName;
    public string Filename = "Screenshot";
    public string Path;
    [HideInInspector] public string FilePath;
    [HideInInspector] public BaseResolution BaseResolution;
    [HideInInspector] public int Width;
    [HideInInspector] public int Height;

    private void Start()
    {
        TakeScreenshot();
    }

    public virtual void TakeScreenshot()
    {
        UpdateResolution();
        PrepareScreenshot();
    }

    private void UpdateResolution()
    {
        var br = BaseResolution.ResolutionWrapper[(int)ResolutionType];
        Width = br.Width;
        Height = br.Height;
    }

    public virtual void PrepareScreenshot()
    {
        Camera.cullingMask = LayerMask.GetMask(LayersName);

        FilePath = string.Format(Path + "/" + Filename + ".png");
    }

    public virtual void ChangePath()
    {
        Path = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
    }

    // public void UpdateTypeTexture()
    // {
    //     var importers = (TextureImporter)AssetImporter.GetAtPath(FilePath);
    //     // Debug.Log("Assets/" + Filename + ".png");
    //     importers.textureType = TextureImporterType.Sprite;
    //     importers.maxTextureSize = 4096;
    //     AssetDatabase.ImportAsset(importers.assetPath, ImportAssetOptions.ForceUpdate);
    // }
}

#endif
