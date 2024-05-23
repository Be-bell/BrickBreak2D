using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitch : MonoBehaviour
{
    public Sprite normalImage; 
    public Sprite hoverImage; 
    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalImage; 
    }

    public void OnMouseEnter()
    {
        buttonImage.sprite = hoverImage;
        Debug.Log("»£√‚");
    }

    public void OnMouseExit()
    {
        buttonImage.sprite = normalImage; 
    }

    public void StartGame()
    {
        DataManager.instance.StartGame();
    }
}
