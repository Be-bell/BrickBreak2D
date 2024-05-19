using System;
using UnityEngine;

 // Icollidable(gameobject ball) ��� �ޱ�
public class Item : MonoBehaviour
{
    protected virtual void ItemEffect(GameObject ball)
    {
        Debug.Log("������ ȿ�� �ߵ�");
    }
    protected void ItemDestroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemEffect(collision.gameObject);
    }
}


