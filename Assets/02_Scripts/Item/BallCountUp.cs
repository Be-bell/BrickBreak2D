using UnityEngine;

public class BallCountUp : Item
{
    [SerializeField]private int ballCount = 2; //일단 2개만 복제
    [SerializeField] private GameObject ball;
    protected override void ItemEffect(GameObject obj)
    {
        for(int i=0; i<ballCount;i++)
        {
            Instantiate(ball);
        }
        ItemDestroy();
    }
      
}


