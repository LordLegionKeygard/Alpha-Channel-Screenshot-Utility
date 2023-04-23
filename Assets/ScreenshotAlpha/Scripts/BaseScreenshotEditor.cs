using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(BaseScreenshot),true)]
public class BaseScreenshotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BaseScreenshot baseScreenshot = (BaseScreenshot)target;
        if(GUILayout.Button("Browse"))
        {
            baseScreenshot.ChangePath();
        }
    }

    static public void RunForChild(AlphaChannelScreenshot a)
    {
        BaseScreenshot baseScreenshot = a;
    }
}

#endif