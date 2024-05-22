using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] public string MainScene;
    [SerializeField] public string StartScene;

    public void SceneMove(int scene)
    {
        GameManager.instance.Score = 0;
        gameObject.SetActive(false);
        if (scene == (int) SceneNumber.MAIN_SCENE)
        {
            DataManager.instance.StartGame();
            SceneManager.LoadScene(MainScene);
        }
        if (scene == (int) SceneNumber.START_SCENE)
        {
            SceneManager.LoadScene(StartScene);
        }
    }

}