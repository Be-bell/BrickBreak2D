using UnityEngine;

public class BallSizeUp:Item
{
    private float ballSize=1f;
  
    protected override void ItemEffect(GameObject ball)
    {
        ball.transform.localScale += new Vector3(ballSize, ballSize,0);
        ItemDestroy();
    } 
}


