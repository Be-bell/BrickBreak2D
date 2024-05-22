using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject stageClear;
    [SerializeField] private GameObject gameOver;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Open(GameState gameState)
    {
        if (gameState == GameState.GAME_CLAER)
        {
            stageClear.SetActive(true);
        }
        else if (gameState == GameState.GAME_OVER)
        {
            gameOver.SetActive(true);
        }
    }

}


