using UnityEngine;

public class BackWall : MonoBehaviour, Icollidable
{
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameManager.instance;
    }
    //[SerializeField] private LayerMask ballLayer;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // ���̾�� ��ġ�ϴ��� ó��
    //    if (IsLayerMatched(ballLayer.value, collision.gameObject.layer))
    //    {
    //        GameManager.instance.SubstractBallCount();
    //        GameObject.Destroy(collision.gameObject);
    //        //collision.gameObject.SetActive(false);
    //    }
    //}

    //private bool IsLayerMatched(int layerMask, int objectLayer)
    //{
    //    return layerMask == (layerMask | (1 << objectLayer));
    //}

    public void OnCollide(GameObject ball)
    {
        ball.SetActive(false);
        gameManager.NotifyBallBreakEvent();
    }
}