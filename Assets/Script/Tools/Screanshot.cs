using System.IO;
using UnityEngine;
using System;

public static class Screanshot
{
    static private Camera cam;

    static int counter = 0;
    static string Path;
    
    public static void Init(string path)
    {
        counter = 0;

        Path = path;
    }

    private static string FormatttedFileName(string dir, string fileExtention = "png")
    {
        return string.Format("{0}{1}.{2}", dir, (++counter).ToString(), fileExtention);
    }

    private static void NotifyConsole(string filename)
    {
        Debug.Log(string.Format("Saved screenshot to:\n{0}", filename));
    }

    public static Camera GetActiveCamera()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        
        if (cam == null) cam = Camera.current;
        if (cam == null) cam = Camera.main;
        if (cam == null) Debug.LogError("No Camera Found in Scene!");

        return cam;
    }

    private static void QuickRender(Vector2 renderSize, int enlarge)
    {
        var tex = RenderToTexture(renderSize, enlarge);
        byte[] bytes = tex.EncodeToPNG(); //Final texture data as bytes
        UnityEngine.Object.DestroyImmediate(tex);
        var dir = Path;
        string filename = FormatttedFileName(dir);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.WriteAllBytes(filename, bytes);
        NotifyConsole(filename);
    }

    private static void QuickRender(Vector2 renderSize, int enlarge, Color backgroundColor)
    {
        var tex = RenderToTexture(renderSize, enlarge, backgroundColor);
        byte[] bytes = tex.EncodeToPNG(); //Final texture data as bytes
        UnityEngine.Object.DestroyImmediate(tex);
        var dir = Path;
        string filename = FormatttedFileName(dir);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.WriteAllBytes(filename, bytes);
        NotifyConsole(filename);
    }

    private static Texture2D RenderToTexture(Vector2 renderSize, int enlarge)
    {
        int w = (int)renderSize.x * enlarge;
        int h = (int)renderSize.y * enlarge;
        RenderTexture rt = new RenderTexture(w, h, 24);
        Texture2D tex = new Texture2D(w, h, TextureFormat.ARGB32, false);
        cam.targetTexture = rt;
        cam.Render();
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        //tex.Apply();//works but, not req and code is faster
        cam.targetTexture = null;
        RenderTexture.active = null;
        UnityEngine.Object.DestroyImmediate(rt);
        return tex;
    }

    private static Texture2D RenderToTexture(Vector2 renderSize, int enlarge, Color backgroundColor)
    {
        return RemoveBackground(RenderToTexture(renderSize, enlarge), backgroundColor);
    }

    private static Texture2D RemoveBackground(Texture2D tex, Color backgroundColor)
    {
        Color[] pixels = tex.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] == backgroundColor)
            {
                pixels[i] = Color.clear;
            }
        }
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }

    public static void TakeScreenshot(GameObject target, Vector2 renderSize, int enlarge, Color backgroundColor)
    {
        if (target == null)
        {
            Debug.LogError("target is null");
            return;
        }

        GetActiveCamera();

        QuickRender(renderSize, enlarge, backgroundColor);
    }

    public static void TakeScreenshot(GameObject target, Vector2 renderSize, int enlarge)
    {
        if (target == null)
        {
            Debug.LogError("target is null");
            return;
        }

        GetActiveCamera();

        QuickRender(renderSize, enlarge);
    }
}