using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite normalSprite; 
    public Sprite hoverSprite; 

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    //private void OnMouseEnter()
    //{
    //    spriteRenderer.sprite = hoverSprite; 
    //}

    //private void OnMouseExit()
    //{
    //    spriteRenderer.sprite = normalSprite; 
    //}
}
