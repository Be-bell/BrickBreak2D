using System;
using UnityEngine;

public class Item : MonoBehaviour, Icollidable
{
    protected virtual void ItemEffect(GameObject ball)
    {
        Debug.Log("������ ȿ�� �ߵ�");
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



