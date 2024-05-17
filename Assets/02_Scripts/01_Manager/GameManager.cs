using UnityEngine;
/// <summary>
/// 게임 로직 구성
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private ObjectPool objPool;
    [SerializeField] private string ballTag;

    // brick class 만들면 추후 기능 추가 예정
    [SerializeField] private string brickTag;

    // ball 개수
    private int ballCount;

    public GameState nowState = GameState.GAME_READY;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }

        BallInstantiate();
        Time.timeScale = 1.0f;
    }

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
    public void SubstractBallCount()
    {
        ballCount--;
    }

    private void FixedUpdate()
    {
        // ball 개수, brick 개수 세서 GameClaer나 GameOver 구현
        GameUpdate();
    }

    // 블록 개수 0개 이하면 GameOver
    private void GameUpdate()
    {
        if (ballCount <= 0)
        {
            Time.timeScale = 0.0f;
            Debug.Log("게임 오버");
            nowState = GameState.GAME_OVER;
        }

        Debug.Log(nowState);
        
    }
    
}
