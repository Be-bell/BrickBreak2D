using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IBrickGradient
{
    BrickGenorator BrickGenorator { get;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void blueredgreen()
    {
        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0].color = Color.red;
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = Color.green;
        colorKeys[1].time = 0.5f;
        colorKeys[2].color = Color.blue;
        colorKeys[2].time = 1.0f;

        // 그라데이션의 알파 값을 설정 (옵션)
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 0.5f;
        alphaKeys[1].time = 1.0f;

        BrickGenorator.customGradient.SetKeys(colorKeys, alphaKeys);

    }
}
