using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGenerate : MonoBehaviour //블럭 프리팹에 붙일 것
{
    public List<GameObject> Item = new List<GameObject>(); //아이템 프리팹 넣기
    private int itemCount;
    [SerializeField]private int probability= 1; //확률 수정

    private void Start()
    {
        itemCount=Item.Count;
    }
    public void ItemCreate(Transform pos)
    {
        int random = Random.Range(0, 100);

        if (random < probability)
        {
            random = Random.Range(0, itemCount); //아이템들중 하나 자동 생성
            Instantiate(Item[random], pos.position, Quaternion.identity);
        }
    }

}