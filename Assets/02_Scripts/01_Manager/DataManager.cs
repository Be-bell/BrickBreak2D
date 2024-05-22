using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 단계 결정, 데이터 전달
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    //데이터 담기
    [SerializeField] private GameInformation gameInfo;
    private List<LevelData> levelDataList;

    //데이터 전달 event
    public event Action<GameState> GameManagerStart;
    public event Action<GameState> ObjectPoolStart;

    [SerializeField] private GameObject wallObj;
    [SerializeField] private string nextSceneName;

    //현재 레벨에 대한 데이터
    public LevelData currentLevelData { get; private set; }

    //현재 레벨
    public GameLevel level;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //첫 레벨 시작 = EASY
        level = GameLevel.EASY;
    }

    private void SetLevelData()
    {
        levelDataList = Instantiate(gameInfo).dataList;
        for (int i = 0; i < levelDataList.Count; i++)
        {
            if (levelDataList[i].level == level)
                currentLevelData = levelDataList[i];
        }
    }


    // 게임 시작 버튼, 시작 누르면 게임시작 창에서 오브젝트 생성.
    public void StartGame()
    {
        SetLevelData();
        StartCoroutine(GameManagerSpawn());
    }

    private IEnumerator GameManagerSpawn()
    {
        AsyncOperation sceneOp = SceneManager.LoadSceneAsync(nextSceneName);
        yield return sceneOp;
        yield return new WaitUntil(SettingObjectPool);
        yield return new WaitUntil(SettingGameManager);
        yield return new WaitUntil(SettingWalls);
    }

    private bool SettingObjectPool()
    {
        ObjectPoolStart.Invoke(GameState.GAME_START);
        return true;
    }

    private bool SettingGameManager()
    {
        GameManagerStart.Invoke(GameState.GAME_START);
        return true;
    }

    private bool SettingWalls()
    {
        Instantiate(wallObj);
        return true;
    }
}