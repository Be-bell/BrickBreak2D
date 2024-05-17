using System;
using UnityEngine;

 // Icollidable(gameobject ball) 상속 받기
public class Item : MonoBehaviour, Icollidable
{
    protected virtual void ItemEffect(GameObject ball)
    {
        Debug.Log("아이템 효과 발동");
    }
    protected void ItemDestroy()
    {
        Destroy(this.gameObject);
    }
    
    public void OnCollide(GameObject ball)
    {
        ItemEffect(ball);
    }

}



