using System;
using UnityEngine;

public class BallDamageUp:Item
{
    private float ballDamage;
    protected override void ItemEffect(GameObject ball)
    {
        Debug.Log("공의 데미지를 증가시킵니다.");
        //Ball ball = ball.GetComponent<Ball>();
        //ball.damage += ballDamage;
        Destroy(this.gameObject);
    }
}