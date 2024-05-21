using System;
using UnityEngine;
/// <summary>
/// ���� ���� ����
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private DataManager dataManager;

    // �̺�Ʈ ����
    private event Action ballBreakEvent;
    public event Action blockBreakEvent;
    private event Action<LevelData?> levelEvent;

    // ����ȭ�ʵ� ����
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

    // ball ���� �� ������.
    private void ballDestroy()
    {
        ballCount--;
        if(ballCount == 0) GameOver();
    }

    private void GameClear()
    {
        // ���⼭���� UI ���� ��.
        Debug.Log("���� Ŭ����!");
        nowState = GameState.GAME_CLAER;
        if(dataManager.level != GameLevel.HARD)
        {
            setData(dataManager.currentLevelData);
        }
        else
        {
            Debug.Log("������ ��� Ŭ�����Ͽ����ϴ�.");
        }
        Time.timeScale = 0.0f;
    }

    private void GameOver()
    {
        // ���⼭���� UIâ ���� ��.
        Debug.Log("���� ����");
        nowState = GameState.GAME_OVER;
        Time.timeScale = 0.0f;
    }

    /*****************************************************************************************************/

    private void Start()
    {
        dataManager = DataManager.instance;
        dataManager.GameManagerStart += GameStart;
    }

    // ���� ����
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

    // ball ���� (Item �������� ��밡��.)
    public void BallInstantiate()
    {
        GameObject ball = objPool.SpawnFromPool(ballTag);
        ballCount++;
        ball.SetActive(true);

        // ball ��ġ ���� ����
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
