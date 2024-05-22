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
            GameClearPanel.SetActive(true);
            Instantiate(GameClearPanel);
        }
        else if (state == GameState.GAME_OVER)
        {
            GameOverPanel.SetActive(true);
            Instantiate(GameOverPanel);
        }
    }
}