using UnityEngine;
/// <summary>
/// ���� ���� ����
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private ObjectPool objPool;
    [SerializeField] private string ballTag;

    // brick class ����� ���� ��� �߰� ����
    [SerializeField] private string brickTag;

    // ball ����
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
    public void SubstractBallCount()
    {
        ballCount--;
    }

    private void FixedUpdate()
    {
        // ball ����, brick ���� ���� GameClaer�� GameOver ����
        GameUpdate();
    }

    // ��� ���� 0�� ���ϸ� GameOver
    private void GameUpdate()
    {
        if (ballCount <= 0)
        {
            Time.timeScale = 0.0f;
            Debug.Log("���� ����");
            nowState = GameState.GAME_OVER;
        }

        Debug.Log(nowState);
        
    }
    
}
