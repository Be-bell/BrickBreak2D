using UnityEngine;

public class BackWall : MonoBehaviour, Icollidable
{
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameManager.instance;
    }

    public void OnCollide(GameObject ball)
    {
        ball.SetActive(false);
        gameManager.ballDestroy();
    }
}