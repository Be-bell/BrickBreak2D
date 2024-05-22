using UnityEngine;

public class PaddleSizeUp:Item
{
    private float size=0.1f;
  
    protected override void ItemEffect(GameObject obj)
    {
        obj.transform.localScale += new Vector3(size, 0, 0);
        ItemDestroy();
    } 
}


