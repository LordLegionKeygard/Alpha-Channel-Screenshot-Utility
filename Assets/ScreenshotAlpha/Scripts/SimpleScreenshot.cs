using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class SimpleScreenshot : BaseScreenshot
{
    public override void PrepareScreenshot()
    {
        base.PrepareScreenshot();

        SimpleCaptureTransparentScreenshot(Camera, Width, Height);
    }

    public void SimpleCaptureTransparentScreenshot(Camera cam, int width, int height)
    {
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);

        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        cam.backgroundColor = Color.clear;
        cam.Render();
        tex_transparent.ReadPixels(grab_area, 0, 0);
        tex_transparent.Apply();

        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        File.WriteAllBytes(FilePath, pngShot);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);

        Texture2D.Destroy(tex_transparent);

        AssetDatabase.Refresh();
    }
}
