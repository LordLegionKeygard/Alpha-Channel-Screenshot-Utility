using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AlphaChannelScreenshot))]
public class AlphaChannelScreenshotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BaseScreenshot baseScreenshot = (AlphaChannelScreenshot)target;
        if (GUILayout.Button("Browse"))
        {
            baseScreenshot.ChangePath();
        }
    }
}
