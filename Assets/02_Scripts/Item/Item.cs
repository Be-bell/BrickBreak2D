using System;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour, Icollidable
{
    private float aniSpeed = 3f;
    private float endY = -10f;
    Sequence itemSequence;

   
    void Start()
    {
        itemSequence = DOTween.Sequence()
        .Append(transform.DOMoveY(endY, aniSpeed));
    }
    protected virtual void ItemEffect(GameObject ball)
    {
        Debug.Log("������ ȿ�� �ߵ�");
    }
    protected void ItemDestroy()
    {
        itemSequence.Kill(this.transform);
        //transform.DOKill(this.transform);
        Destroy(this.gameObject);
    }
    
    public void OnCollide(GameObject obj)
    {
        ItemEffect(obj);
    }

}

