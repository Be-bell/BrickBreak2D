using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 게임 로직 구성
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private DataManager dataManager;
    private ObjectPool objectPool;

    // 이벤트 관리
    //public event Action ballBreakEvent;
    //public event Action blockBreakEvent;
    //public event Action<LevelData?> levelEvent;
    //public event Action<GameState> gameStateEvent;

    // 직렬화필드 관리
    [SerializeField] private string ballTag;
    [SerializeField] private string brickTag;
    
    public GameState nowState = GameState.GAME_READY;

    [SerializeField] private int blockCount;
    [SerializeField] private int ballCount;
    private List<GameObject> ballList;
    private List<GameObject> brickList;

    public int Score;

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
    }

    private void setData(LevelData data)
    {
        blockCount = data.bricksNum;
        ballCount = 0;
    }

    public void blockDestroy()
    {
        blockCount--;
        Score += 10;
        if (blockCount <= 0) GameClear();
    }

    // ball 삭제 후 따지기.
    public void ballDestroy()
    {
        ballCount--;
        if(ballCount <= 0) GameOver();
    }

    private void GameClear()
    {
        // 여기서부터 UI 띄우는 거.
        Debug.Log("게임 클리어!");
        nowState = GameState.GAME_CLAER;
        if(dataManager.level != GameLevel.HARD)
        {
            dataManager.level++;
        }
        else
        {
            Debug.Log("게임을 모두 클리어하였습니다.");
        }
        blockCount = 0;
        UIManager.Instance.Open(nowState);
        DisActive();
        Time.timeScale = 0.0f;
        UIManager.Instance.Open(nowState);
    }

    private void GameOver()
    {
        // 여기서부터 UI창 띄우는 거.
        Debug.Log("게임 오버");
        nowState = GameState.GAME_OVER;
        UIManager.Instance.Open(nowState);
        DisActive();
        Time.timeScale = 0.0f;
        UIManager.Instance.Open(nowState);
    }


    /*****************************************************************************************************/

    private void Start()
    {
        dataManager = DataManager.instance;
        objectPool = ObjectPool.instance;
        //gameEnd = GetComponent<GameEnd>();
        //dataManager.GameManagerStart += GameStart;
    }

    // 게임 시작
    public void GameStart(GameState state, LevelData data)
    {
        ballList = new List<GameObject>();
        if(state == GameState.GAME_START)
        {
            Time.timeScale = 1.0f;
            setData(data);
            BlockSetting(data.bricksNum);
            BallInstantiate();
            //ballBreakEvent += ballDestroy;
            //blockBreakEvent += blockDestroy;
        }
    }

    private void BlockSetting(int bricksNum)
    {
        float initX = -2.3f;
        float initY = 4f;

        brickList = new List<GameObject>();
        for(int i=0; i<bricksNum; i++)
        {
            Vector3 pos = new Vector3(initX + i % 6 * 0.92f, initY - (i / 6) * 0.5f, 0f);
            GameObject brick = objectPool.SpawnFromPool(brickTag);
            brick.SetActive(true);
            brickList.Add(brick);
            brick.transform.position = pos;
        }
    }

    private void DisActive()
    {
        foreach(GameObject obj in brickList)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in ballList)
        {
            obj.SetActive(false);
        }
    }

    // ball 생성 (Item 로직에서 사용가능.)
    public void BallInstantiate()
    {
        GameObject ball = objectPool.SpawnFromPool(ballTag);
        // ball 위치 추후 수정
        ball.transform.position = new Vector3(0, -4, 0);
        ball.SetActive(true);
        ballList.Add(ball);

        ballCount++;

    }

    /*****************************************************************************************************/

    //public void NotifyBallBreakEvent()
    //{
    //    ballBreakEvent?.Invoke();
    //}

    //public void NotifyBlockBreakEvent()
    //{
    //    blockBreakEvent?.Invoke();
    //}

    //public void NotifyLevelEvent(LevelData? data)
    //{
    //    levelEvent?.Invoke(data);
    //}

    //private void NotifyGameStateEvent(GameState data)
    //{
    //    gameStateEvent?.Invoke(data);
    //}

    /*****************************************************************************************************/
}
