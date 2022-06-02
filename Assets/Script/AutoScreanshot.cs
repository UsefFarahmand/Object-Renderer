using EasyButtons;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AutoScreanshot : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Save Destination"), Tooltip("Root save folder location.")]
    private string subDirectory = "Renderer";

    [Header("Render setting")]
    [SerializeField] private Vector2 renderSize = new Vector2(800, 600);
    [Range(1, 10), SerializeField] private int enlarge = 1;

    [Header("Background")]
    [SerializeField] private bool removeBackground = true;
    [SerializeField, ConditionalHide(nameof(removeBackground), true)] private Color backgroundColor;

    [Header("Screanshot Data")]
    [SerializeField] private List<TextAsset> textAssets;

    [Header("Sprite Sheet Creator")]
    [SerializeField] private List<GameObject> targets;

    [Button("Take Screenshot", Spacing = ButtonSpacing.Before)]
    private void TakeScreenshot()
    {
        if (textAssets.Count == 0)
        {
            Debug.LogError("textAssets is empty");
            return;
        }
        if (targets.Count == 0)
        {
            Debug.LogError("targets is empty");
            return;
        }

        foreach (var target in targets)
        {
            var obj = Instantiate(target);
            foreach (var textAsset in textAssets)
            {
                var datas = ScreanshotDataManager.Load(textAsset);
                var path = Application.dataPath + "/../" + $"{subDirectory}/{target.name}/{textAsset.name}" + "/";
                Screanshot.Init(path);
                foreach (var data in datas)
                {
                    obj.transform.localPosition = data.Position;
                    obj.transform.localRotation = data.Rotation;
                    if (removeBackground)
                    {
                        Screanshot.TakeScreenshot(obj, renderSize, enlarge, backgroundColor);
                    }
                    else
                    {
                        Screanshot.TakeScreenshot(obj, renderSize, enlarge);
                    }
                }
            }
            DestroyImmediate(obj.gameObject);
        }

        Debug.Log("All photos were taken!!");
    }

    [Button(Spacing = ButtonSpacing.Before)]
    public void ShowExplorer()
    {
        var path = Application.dataPath + "/../" + $"{subDirectory}/";
        if (!Directory.Exists(path))
        {
            Debug.LogError($"Path {path} is not exist");
            return;
        }
        path = path.Replace(@"/", @"\");
        System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
    }

    private void Reset()
    {
        name = "Auto Screanshot";

        renderSize = new Vector2(1080, 1080);
    }
#endif
}
