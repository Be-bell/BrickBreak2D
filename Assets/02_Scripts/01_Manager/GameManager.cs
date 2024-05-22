using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ���� ����
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private DataManager dataManager;
    private ObjectPool objectPool;

    // �̺�Ʈ ����
    //public event Action ballBreakEvent;
    //public event Action blockBreakEvent;
    //public event Action<LevelData?> levelEvent;
    //public event Action<GameState> gameStateEvent;

    // ����ȭ�ʵ� ����
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

    // ball ���� �� ������.
    public void ballDestroy()
    {
        ballCount--;
        if(ballCount <= 0) GameOver();
    }

    private void GameClear()
    {
        // ���⼭���� UI ���� ��.
        Debug.Log("���� Ŭ����!");
        nowState = GameState.GAME_CLAER;
        if(dataManager.level != GameLevel.HARD)
        {
            dataManager.level++;
        }
        else
        {
            Debug.Log("������ ��� Ŭ�����Ͽ����ϴ�.");
        }
        blockCount = 0;
        UIManager.Instance.Open(nowState);
        DisActive();
        Time.timeScale = 0.0f;
        UIManager.Instance.Open(nowState);
    }

    private void GameOver()
    {
        // ���⼭���� UIâ ���� ��.
        Debug.Log("���� ����");
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

    // ���� ����
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

    // ball ���� (Item �������� ��밡��.)
    public void BallInstantiate()
    {
        GameObject ball = objectPool.SpawnFromPool(ballTag);
        // ball ��ġ ���� ����
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
