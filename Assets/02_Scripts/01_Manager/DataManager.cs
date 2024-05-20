using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 단계 결정, 데이터 전달
/// </summary>
public class DataManager : MonoBehaviour
{
    DataManager instance;

    public GameLevel level { get; private set; }
    [SerializeField] private GameInformation gameInfo;
    private List<LevelData> levelDataList;
    public LevelData? currentLevelData { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        level = GameLevel.EASY;

    }

    private void Start()
    {
        SetLevelData();   
    }

    private void SetLevelData()
    {
        levelDataList = Instantiate(gameInfo).dataList;
        for (int i = 0; i < levelDataList.Count; i++)
        {
            currentLevelData = levelDataList[i].level == level ? levelDataList[i] : null;
        }
    }
}