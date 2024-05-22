using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndButtons : MonoBehaviour
{
    [SerializeField] private string StartSceneName;
    [SerializeField] private string MainSceneName;
    public void StartScene()
    {
        Time.timeScale = 1.0f;
        GameManager.instance.nowState = GameState.GAME_READY;
        gameObject.SetActive(false);
        SceneManager.LoadScene(StartSceneName);
        Destroy(gameObject);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        DataManager.instance.StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }
}