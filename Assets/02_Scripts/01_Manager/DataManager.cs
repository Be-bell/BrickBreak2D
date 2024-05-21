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
    public event Action<GameState> GameManagerStart;
    public event Action<GameState> ObjectPoolStart;

    [SerializeField] private GameObject wallObj;
    [SerializeField] private string nextSceneName;

    public LevelData currentLevelData { get; private set; }
    public GameLevel level;

    [SerializeField] private GameInformation gameInfo;
    private List<LevelData> levelDataList;
    private ObjectPool pool;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        level = GameLevel.EASY;
        SetLevelData();
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
        Debug.Log("실행완료");
        StartCoroutine(GameManagerSpawn());
    }

    IEnumerator GameManagerSpawn()
    {
        AsyncOperation sceneOp = SceneManager.LoadSceneAsync(nextSceneName);
        yield return sceneOp;
        yield return new WaitUntil(SettingObjectPool);
        yield return new WaitUntil(SettingGameManager);
        yield return new WaitUntil(SettingWalls);
    }

    private bool SettingWalls()
    {
        Instantiate(wallObj);
        return true;
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

    
}