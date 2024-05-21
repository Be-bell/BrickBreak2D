using UnityEngine;

public class BackWall : MonoBehaviour
{
    [SerializeField] private LayerMask ballLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���̾�� ��ġ�ϴ��� ó��
        if (IsLayerMatched(ballLayer.value, collision.gameObject.layer))
        {
            GameManager.instance.SubstractBallCount();
            GameObject.Destroy(collision.gameObject);
        }
    }

    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}