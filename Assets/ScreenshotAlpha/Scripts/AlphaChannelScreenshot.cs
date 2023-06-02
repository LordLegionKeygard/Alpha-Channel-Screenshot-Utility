using UnityEngine;
using System.IO;
using System;
using UnityEditor;

#if UNITY_EDITOR

public class AlphaChannelScreenshot : BaseScreenshot
{
    public override void PrepareScreenshot()
    {
        base.PrepareScreenshot();
        CaptureTransparentScreenshot(Camera, Width, Height);
    }

    public void CaptureTransparentScreenshot(Camera cam, int width, int height)
    {
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_white = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var tex_black = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);

        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        cam.backgroundColor = Color.black;
        cam.Render();
        tex_black.ReadPixels(grab_area, 0, 0);
        tex_black.Apply();

        cam.backgroundColor = Color.white;
        cam.Render();
        tex_white.ReadPixels(grab_area, 0, 0);
        tex_white.Apply();


        for (int y = 0; y < tex_transparent.height; ++y)
        {
            for (int x = 0; x < tex_transparent.width; ++x)
            {
                float alpha = tex_white.GetPixel(x, y).r - tex_black.GetPixel(x, y).r;
                alpha = 1.0f - alpha;
                Color color;

                if (alpha == 0) color = Color.clear;

                else color = tex_black.GetPixel(x, y) / alpha;

                color.a = alpha;
                tex_transparent.SetPixel(x, y, color);
            }
        }

        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        File.WriteAllBytes(FilePath, pngShot);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);

        Texture2D.Destroy(tex_black);
        Texture2D.Destroy(tex_white);
        Texture2D.Destroy(tex_transparent);

        AssetDatabase.Refresh();

        // UpdateTypeTexture();
    }
}

#endif