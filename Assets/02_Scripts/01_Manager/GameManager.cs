using System;
using UnityEngine;
/// <summary>
/// ���� ���� ����
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // �̺�Ʈ ����
    private event Action ballBreakEvent;
    private event Action blockBreakEvent;
    private event Action<LevelData?> levelEvent;

    // ����ȭ�ʵ� ����
    [SerializeField] public ObjectPool objPool;
    [SerializeField] private string ballTag;
    // brick class ����� ���� ��� �߰� ����
    //[SerializeField] private string brickTag;

    public GameState nowState = GameState.GAME_READY;
    private int blockCount;
    private int ballCount;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        ballBreakEvent += ballDestroy;
        blockBreakEvent += blockDestroy;
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
        BallInstantiate();
        Time.timeScale = 1.0f;
    }

    // ball ���� (Item �������� ��밡��.)
    public void BallInstantiate()
    {
        GameObject ball = objPool.SpawnFromPool(ballTag);
        ballCount++;
        ball.SetActive(true);

        // ball ��ġ ���� ����
        ball.transform.position = Vector3.zero;
    }

    // ball count ����. ���� �� ���� ���� �ִ��� Ȯ��
    //public void SubstractBallCount()
    //{
    //    ballCount--;
    //}

    //private void FixedUpdate()
    //{
    //    // ball ����, brick ���� ���� GameClaer�� GameOver ����
    //    GameUpdate();
    //}

    //// ��� ���� 0�� ���ϸ� GameOver
    //private void GameUpdate()
    //{
    //    if (ballCount <= 0)
    //    {
    //        Time.timeScale = 0.0f;
    //        Debug.Log("���� ����");
    //        nowState = GameState.GAME_OVER;
    //    }

    //    Debug.Log(nowState);

    //}

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

}
