using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndButtons : MonoBehaviour
{
    [SerializeField] private string StartSceneName;
    [SerializeField] private string MainSceneName;
    public void StartScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(StartSceneName);
        GameManager.instance.nowState = GameState.GAME_READY;
        gameObject.SetActive(false);
        gameObject.IsDestroyed();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(MainSceneName);
        GameManager.instance.nowState = GameState.GAME_START;
        gameObject.SetActive(false);
        gameObject.IsDestroyed();
    }
}