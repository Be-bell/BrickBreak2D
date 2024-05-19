using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = new GameManager();
        }

        DontDestroyOnLoad(gameObject);

    }
}

public enum GameState
{
    GAME_START,
    GAME_END,
    GAME_PAUSE
}
