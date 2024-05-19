using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenorator : MonoBehaviour, IBrickPattern
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPreFab;
    public Gradient gradient;

    // Start is called before the first frame update
    public void Awake()
    {

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                // 체크 모양을 생성하는 조건 추가
                if ((i + j) % 2 == 0)
                {
                    GameObject newBrick = Instantiate(brickPreFab, transform);
                    newBrick.transform.position = transform.position + new Vector3((float)((size.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0);
                    newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));
                }
            }
        }
    }
    

}
