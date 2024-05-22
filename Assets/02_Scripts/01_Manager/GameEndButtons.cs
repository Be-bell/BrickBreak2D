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
        GameManager.instance.nowState = GameState.GAME_READY;
        gameObject.SetActive(false);
        gameObject.IsDestroyed();
        SceneManager.LoadScene(StartSceneName);
        
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        DataManager.instance.StartGame();
        gameObject.SetActive(false);
        gameObject.IsDestroyed();
        SceneManager.LoadScene(MainSceneName);
        
    }
}