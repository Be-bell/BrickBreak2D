using System;
using UnityEngine;
/// <summary>
/// 게임 로직 구성
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 이벤트 관리
    private event Action ballBreakEvent;
    private event Action blockBreakEvent;

    // 직렬화필드 관리
    [SerializeField] private ObjectPool objPool;
    [SerializeField] private string ballTag;
    // brick class 만들면 추후 기능 추가 예정
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

    // ball 생성 (Item 로직에서 사용가능.)
    public void BallInstantiate()
    {
        GameObject ball = objPool.SpawnFromPool(ballTag);
        ballCount++;
        ball.SetActive(true);

        // ball 위치 추후 수정
        ball.transform.position = Vector3.zero;
    }

    // ball count 빼기. 조금 더 좋은 생각 있는지 확인
    //public void SubstractBallCount()
    //{
    //    ballCount--;
    //}

    //private void FixedUpdate()
    //{
    //    // ball 개수, brick 개수 세서 GameClaer나 GameOver 구현
    //    GameUpdate();
    //}

    //// 블록 개수 0개 이하면 GameOver
    //private void GameUpdate()
    //{
    //    if (ballCount <= 0)
    //    {
    //        Time.timeScale = 0.0f;
    //        Debug.Log("게임 오버");
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
