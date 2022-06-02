using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SheetCreator : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    private void Reset()
    {
        name = "Sprite Sheet Creator";
        sprites.Clear();
    }

    [EasyButtons.Button]
    private void Create()
    {
        Create(sprites);
    }

    public void Create(List<Sprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            SetTextureSetting(sprite.texture);
        }

        int height = sprites[0].texture.height;
        int width = sprites[0].texture.width * sprites.Count;

        var sheet = new Texture2D(width, height);
        var rects = new List<Rect>();
        var textures = new List<Texture2D>();
        for (int i = 0; i < sprites.Count; i++)
        {
            var rect = new Rect(sprites[i].textureRect);
            rect.x *= sprites[i].texture.width;
            rect.y *= sprites[i].texture.height;
            rect.width *= sprites[i].texture.width;
            rect.height *= sprites[i].texture.height;
            rects.Add(rect);
            textures.Add(sprites[i].texture);
        }

        sheet.PackTextures(textures.ToArray(), 10);
        DeCompress(sheet);
        var bytes = sheet.EncodeToPNG();
        //Application.dataPath + "/../" + SubDirectory + "/";
        var path = Application.dataPath + "/" + sprites[0].name + ".png";
        Debug.Log(path);
        System.IO.File.WriteAllBytes(path, bytes);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void SetTextureSetting(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter A = (TextureImporter)AssetImporter.GetAtPath(path);
        A.filterMode = FilterMode.Point;
        A.isReadable = true;
        A.textureCompression = TextureImporterCompression.Uncompressed;
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }

    public Texture2D DeCompress(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
