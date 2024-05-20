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

    // ����ȭ�ʵ� ����
    [SerializeField] private ObjectPool objPool;
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

        BallInstantiate();
        ballBreakEvent += ballDestroy;
        blockBreakEvent += blockDestroy;
        Time.timeScale = 1.0f;
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

}
