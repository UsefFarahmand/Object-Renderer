using EasyButtons;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ManualScreanshot : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Save Destination"), Tooltip("Root save folder location.")]
    [SerializeField, ReadOnly, ContextMenuItem("Show Explorer", "ShowExplorer")] private string path;
    private string subDirectory = "Renderer";

    [Header("Render setting")]
    [SerializeField] private Vector2 renderSize = new Vector2(800, 600);
    [Range(1, 10), SerializeField] private int enlarge = 1;

    [Header("Background")]
    [SerializeField] private bool removeBackground = true;
    [SerializeField, ConditionalHide(nameof(removeBackground), true)] private Color backgroundColor;

    [Header("Sprite Sheet Creator")]
    [SerializeField] private GameObject target;
    [SerializeField] private string spriteSheetName = "";
    [SerializeField] private string folderName = "";

    [Header("Resault")]
    [SerializeField] private List<ScreanshotData> datas;

    [Button("Take Screenshot Now", Spacing = ButtonSpacing.Before)]
    private void TakeScreenshot()
    {
        if (removeBackground)
        {
            Screanshot.TakeScreenshot(target, renderSize, enlarge, backgroundColor);
        }
        else
        {
            Screanshot.TakeScreenshot(target, renderSize, enlarge);
        }
        datas.Add(new ScreanshotData(target.transform.localPosition, target.transform.localRotation));
    }

    [Button(Spacing = ButtonSpacing.Before)]
    private void Save()
    {
        if (datas.Count == 0)
        {
            Debug.LogError("datas is empty");
            return;
        }

        datas.Save(spriteSheetName);
        ManualReset();
    }

    [Button("Reset")]
    private void ManualReset()
    {
        if (datas.Count > 0)
        {
            datas.Clear();
        }
        path = Application.dataPath + "/../" + $"{subDirectory}/{folderName}/{spriteSheetName}" + "/";
        Screanshot.Init(path);
        Debug.Log("Reseted");
    }

    [Button]
    public void ShowExplorer()
    {
        if (folderName == string.Empty)
        {
            Debug.LogError("folderName is empty");
            return;
        }
        if (spriteSheetName == string.Empty)
        {
            Debug.LogError("spriteSheetName is empty");
            return;
        }

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
        Checkbackground();
        name = "Manual Screanshot";

        renderSize = new Vector2(1080, 1080);

        if (datas.Count > 0)
        {
            datas.Clear();
        }
        
        path = Application.dataPath + "/../" + $"{subDirectory}/{folderName}/{spriteSheetName}" + "/";
        Screanshot.Init(path);
    }

    private void OnValidate()
    {
        path = Application.dataPath + "/../" + $"{subDirectory}/{folderName}/{spriteSheetName}" + "/";

        Checkbackground();
    }

    private void Checkbackground()
    {
        if (removeBackground)
        {
            var cam = Screanshot.GetActiveCamera();
            if (cam.clearFlags != CameraClearFlags.SolidColor)
            {
                cam.clearFlags = CameraClearFlags.SolidColor;
                if (ColorUtility.TryParseHtmlString("5AEF23", out Color color))
                {
                    cam.backgroundColor = color;
                    backgroundColor = color;
                }
                else
                {
                    backgroundColor = cam.backgroundColor;
                }
            }

            if (backgroundColor != cam.backgroundColor)
            {
                cam.backgroundColor = backgroundColor;
            }
        }
        else
        {
            var cam = Screanshot.GetActiveCamera();
            cam.clearFlags = CameraClearFlags.Skybox;
        }
    }
#endif    
}
