using UnityEngine;

public class BallCountUp : Item
{
    [SerializeField]private int ballCount = 2; //일단 2개만 복제
    protected override void ItemEffect(GameObject ball)
    {
        for(int i=0; i<ballCount;i++)
        {
            Instantiate(ball);
        }
        Destroy(this.gameObject);
    }
      
}


