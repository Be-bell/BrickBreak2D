using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;

public class ScoreData
{
    RankData rankData = new RankData();
    public void SaveTool<T>(string fileName, T data)
    {
        string path = Application.persistentDataPath + fileName + ".Json";
        var setJson = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, setJson);
    }

    public void LoadTool<T>(string fileName, ref T data)
    {
        string path = Application.persistentDataPath + fileName + ".Json";

        FileInfo fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            Debug.Log("X");
        }
        else
        {
            string json = File.ReadAllText(path);

            data = JsonConvert.DeserializeObject<T>(json);
        }
    }
}