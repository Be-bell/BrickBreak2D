using System;
using UnityEngine;
/// <summary>
/// 게임 로직 구성
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private DataManager dataManager;

    // 이벤트 관리
    private event Action ballBreakEvent;
    public event Action blockBreakEvent;
    private event Action<LevelData?> levelEvent;

    // 직렬화필드 관리
    [SerializeField] public ObjectPool objPool;
    [SerializeField] private string ballTag;
    [SerializeField] private string brickTag;


    public GameState nowState = GameState.GAME_READY;
    [SerializeField] private int blockCount;
    private int ballCount;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        ballBreakEvent += ballDestroy;
        blockBreakEvent += blockDestroy;
        DontDestroyOnLoad(gameObject);
    }

    public void setData(LevelData data)
    {
        blockCount = data.bricksNum;
    }

    private void blockDestroy()
    {
        blockCount--;
        if (blockCount == 0) GameClear();
    }

    // ball 삭제 후 따지기.
    private void ballDestroy()
    {
        ballCount--;
        if(ballCount == 0) GameOver();
    }

    private void GameClear()
    {
        // 여기서부터 UI 띄우는 거.
        Debug.Log("게임 클리어!");
        nowState = GameState.GAME_CLAER;
        if(dataManager.level != GameLevel.HARD)
        {
            setData(dataManager.currentLevelData);
        }
        else
        {
            Debug.Log("게임을 모두 클리어하였습니다.");
        }
        Time.timeScale = 0.0f;
    }

    private void GameOver()
    {
        // 여기서부터 UI창 띄우는 거.
        Debug.Log("게임 오버");
        nowState = GameState.GAME_OVER;
        Time.timeScale = 0.0f;
    }

    /*****************************************************************************************************/

    private void Start()
    {
        dataManager = DataManager.instance;
        dataManager.GameManagerStart += GameStart;
    }

    // 게임 시작
    private void GameStart(GameState state)
    {
        if(state == GameState.GAME_START)
        {
            setData(dataManager.currentLevelData);
            BallInstantiate();
            Time.timeScale = 1.0f;
            BlockSetting(dataManager.currentLevelData.bricksNum);
        }
    }

    private void BlockSetting(int bricksNum)
    {
        float initX = -2.3f;
        float initY = 4f;

        for(int i=0; i<bricksNum; i++)
        {
            Vector3 pos = new Vector3(initX + i % 6 * 0.92f, initY - (i / 4) * 0.5f, 0f);
            GameObject brick = objPool.SpawnFromPool(brickTag);
            brick.transform.position = pos;
            brick.SetActive(true);
        }
    }

    // ball 생성 (Item 로직에서 사용가능.)
    public void BallInstantiate()
    {
        GameObject ball = objPool.SpawnFromPool(ballTag);
        ballCount++;
        ball.SetActive(true);

        // ball 위치 추후 수정
        ball.transform.position = new Vector3(0, -3, 0);
    }

    /*****************************************************************************************************/

    public void NotifyBallBreakEvent()
    {
        ballBreakEvent?.Invoke();
    }

    public void NotifyBlockBreakEvent()
    {
        blockBreakEvent?.Invoke();
    }

    public void NotifyLevelEvent(LevelData? data)
    {
        levelEvent?.Invoke(data);
    }

    /*****************************************************************************************************/

    
}
