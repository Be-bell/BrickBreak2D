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

    [SerializeField] private GameObject objPool;
    [SerializeField] private GameObject gameManagerObj;
    [SerializeField] private GameObject wallObj;
    [SerializeField] private string nextSceneName;

    public LevelData? currentLevelData { get; private set; }
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
            currentLevelData = levelDataList[i].level == level ? levelDataList[i] : null;
        }
    }

    // 게임 시작 버튼, 시작 누르면 게임시작 창에서 오브젝트 생성.
    public void GameStart()
    {
        StartCoroutine(GameManagerSpawn());
    }

    IEnumerator GameManagerSpawn()
    {
        AsyncOperation sceneOp = SceneManager.LoadSceneAsync(nextSceneName);
        yield return sceneOp;
        yield return new WaitUntil(objectPoolSet);
        yield return new WaitUntil(gameManagerSet);
        yield return new WaitUntil(WallSet);
    }

    private bool objectPoolSet()
    {
        GameObject objectPool = Instantiate(objPool);
        objectPool.name = "ObjectPool";
        pool = objectPool.GetComponent<ObjectPool>();
        return objectPool;
    }

    private bool gameManagerSet()
    {
        GameObject gameManager = Instantiate(gameManagerObj);
        gameManager.name = "GameManager";
        GameManager gmManager = gameManager.GetComponent<GameManager>();
        gmManager.NotifyLevelEvent(currentLevelData);
        gmManager.objPool = pool;
        gmManager.nowState = GameState.GAME_START;
        return true;
    }

    private bool WallSet()
    {
        GameObject wall = Instantiate(wallObj);
        wall.name = "wall";
        return true;
    }

}