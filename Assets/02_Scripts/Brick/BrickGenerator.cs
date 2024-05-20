using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenorator : MonoBehaviour, IBrickPattern //IBrickGradient
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPreFab;
    public Gradient gradient;
    public Gradient customGradient; // ����� ���� �׶��̼�
    public Brick brick;
    // Start is called before the first frame update
    public void  Awake()
    {

        for (int i = 0; i < size.x; i++)
        {
            
            for (int j = 0; j < size.y; j++)
            {
                // üũ ����� �����ϴ� ���� �߰�
                if ((i + j) % 2 == 0)
                {
                    
                    // �׶��̼� ������ ���ο� �׶��̼� ����
                    GameObject newBrick = Instantiate(brickPreFab, transform);
                    newBrick.transform.position = transform.position + new Vector3((float)((size.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0);
                    newBrick.GetComponent<SpriteRenderer>().color = customGradient.Evaluate((float)j / (size.y - 1));
                }
            }
        }
    }
    

}
