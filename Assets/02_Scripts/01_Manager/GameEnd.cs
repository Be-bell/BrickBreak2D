using UnityEngine;

public class GameEnd : MonoBehaviour
{

    [SerializeField] private GameObject GameClearPanel;
    [SerializeField] private GameObject GameOverPanel;

    private void Start()
    {
        GameManager.instance.gameStateEvent += gameEnd;
    }

    private void gameEnd(GameState state)
    {
        if(state == GameState.GAME_CLAER)
        {
            GameObject panel = Instantiate(GameClearPanel, transform);
            GameClearPanel.SetActive(true);
        }
        else if (state == GameState.GAME_OVER)
        {
            GameObject panel = Instantiate(GameOverPanel, transform);
            GameOverPanel.SetActive(true);
        }
    }
}