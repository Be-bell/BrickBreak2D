using System;
using System.Collections.Generic;
using GoogleSheetsToUnity;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class DataReaderBase : ScriptableObject
{
    [Header("Spread Sheet Adress")]
    [SerializeField] public string associatedSheet = "";

    [Header("Sheet Name")]
    [SerializeField] public string associatedWorksheet = "";

    [Header("읽기 시작할 행 번호")]
    [SerializeField] public int START_ROW_LENGTH = 2;

    [Header("읽을 마지막 행 번호")]
    [SerializeField] public int END_ROW_LENGTH = -1;
}

[Serializable]
public struct LevelData
{
    public GameLevel level;
    public int bricksNum;
    public int paddleSize;

    public LevelData(GameLevel level, int bricksNum, int paddleSize)
    {
        this.level = level;
        this.bricksNum = bricksNum;
        this.paddleSize = paddleSize;
    }
}



[CreateAssetMenu(fileName = "Reader", menuName = "Spread Sheet/GameInformation", order = int.MaxValue)]
public class GameInformation : DataReaderBase
{
    [Header("Sheet Objects")]
    [SerializeField] public List <LevelData> dataList = new List<LevelData>();

    internal void UpdateStats(List<GSTU_Cell> list, int level_ID)
    {
        GameLevel level = GameLevel.NULL;
        int bricksNum = -1, paddleSize = -1;

        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "GameLevel":
                    {
                        level = (GameLevel)int.Parse(list[i].value);
                        break;
                    }
                case "bricksNum":
                    {
                        bricksNum = int.Parse(list[i].value);
                        break;
                    }
                case "paddleSize":
                    {
                        paddleSize = int.Parse(list[i].value);
                        break;
                    }
            }
        }

        dataList.Add(new LevelData(level, bricksNum, paddleSize));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameInformation))]
public class GameInformationEditor : Editor
{
    GameInformation data;

    private void OnEnable()
    {
        data = (GameInformation)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\nRead Spread Sheet");

        if (GUILayout.Button("Pull data"))
        {
            UpdateStats(UpdateMethodOne);
            data.dataList.Clear();
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        for (int i = data.START_ROW_LENGTH; i <= data.END_ROW_LENGTH; ++i)
        {
            data.UpdateStats(ss.rows[i], i);
        }

        EditorUtility.SetDirty(target);
    }
}
#endif