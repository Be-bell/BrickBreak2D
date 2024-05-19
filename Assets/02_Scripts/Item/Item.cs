using System;
using UnityEngine;

 // Icollidable(gameobject ball) 상속 받기
public class Item : MonoBehaviour
{
    protected virtual void ItemEffect(GameObject ball)
    {
        Debug.Log("아이템 효과 발동");
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


