using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ScreanshotData
{

    public ScreanshotData(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public Vector3 Position;
    public Quaternion Rotation;
}

public static class ScreanshotDataManager
{
    public static void Save(this List<ScreanshotData> datas, string dataPackName)
    {
        if (datas.Count == 0)
        {
            Debug.LogError("datas is empty");
            return;
        }
        var res = Application.dataPath + "/Resources";
        if (!Directory.Exists(res))
        {
            Directory.CreateDirectory(res);
        }
        string path = Application.dataPath + $"/Resources/{dataPackName}.json";
        var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath(path);
        var json = JsonHelper.ToJson(datas.ToArray());
        File.WriteAllText(uniqueFileName, json);

        Debug.Log($"{dataPackName} save successfully in {path}");

        datas.Clear();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static List<ScreanshotData> Load(string spriteSheetName)
    {
        string path = Application.dataPath + $"/Resources/{spriteSheetName}.json";
        var text = File.ReadAllText(path);
        var datas = JsonHelper.FromJson<ScreanshotData>(text);
        return datas.ToList();
    }

    public static List<ScreanshotData> Load(TextAsset textAsset)
    {
        var datas = JsonHelper.FromJson<ScreanshotData>(textAsset.text);
        return datas.ToList();
    }
}