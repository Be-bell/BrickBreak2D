using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �ܰ� ����, ������ ����
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    //������ ���
    [SerializeField] private GameInformation gameInfo;
    private List<LevelData> levelDataList;

    //������ ���� event
    public event Action<GameState> GameManagerStart;
    public event Action<GameState> ObjectPoolStart;

    [SerializeField] private GameObject wallObj;
    [SerializeField] private string nextSceneName;

    //���� ������ ���� ������
    public LevelData currentLevelData { get; private set; }

    //���� ����
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

        //ù ���� ���� = EASY
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


    // ���� ���� ��ư, ���� ������ ���ӽ��� â���� ������Ʈ ����.
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