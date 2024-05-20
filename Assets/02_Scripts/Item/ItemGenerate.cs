using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGenerate : MonoBehaviour //���� �����տ� ���� ��
{
    public List<GameObject> Item = new List<GameObject>(); //������ ������ �ֱ�
    private int itemCount;
    [SerializeField]private int probability= 1; //Ȯ�� ����

    private void Start()
    {
        itemCount=Item.Count;
    }
    void ItemCreate()
    {
        int random = Random.Range(0, 100);

        if (random < probability)
        {
            random = Random.Range(0, itemCount); //�����۵��� �ϳ� �ڵ� ����
            Instantiate(Item[random]);
        }
    }

}