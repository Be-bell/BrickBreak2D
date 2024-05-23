using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject stageClear;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private GameObject ClearGameScore;
    [SerializeField] private GameObject ClearBestScore;

    [SerializeField] private GameObject EndGameScore;
    [SerializeField] private GameObject EndBestScore;

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
        int nowScore = DataManager.instance.nowScore;

        if (gameState == GameState.GAME_CLAER)
        {
            stageClear.SetActive(true);
            TextMeshProUGUI clearGameScoreText = ClearGameScore.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI clearBestScoreText = ClearBestScore.GetComponent<TextMeshProUGUI>();
            clearGameScoreText.text = nowScore.ToString();
            if (nowScore > DataManager.instance.bestScore)
            {
                DataManager.instance.bestScore = nowScore;
            }
            clearBestScoreText.text = DataManager.instance.bestScore.ToString();
            DataManager.instance.lastScore = nowScore;
        }
        else if (gameState == GameState.GAME_OVER)
        {
            gameOver.SetActive(true);
            TextMeshProUGUI endGameScoreText = EndGameScore.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI endGameBestText = EndBestScore.GetComponent<TextMeshProUGUI>();
            endGameScoreText.text = nowScore.ToString();
            if (nowScore > DataManager.instance.bestScore)
            {
                DataManager.instance.bestScore = nowScore;
            }
            endGameBestText.text = DataManager.instance.bestScore.ToString();
            DataManager.instance.lastScore = 0;
        }
    }

}


