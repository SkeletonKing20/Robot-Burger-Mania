using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    SpriteRenderer spriteR;
    [SerializeField]
    Sprite hoverSprite;
    Sprite initialSprite;
    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        initialSprite = spriteR.sprite;
    }
    public void Restart()
        {

        }
        public void MainMenu()
        {

        }

        public void Quit()
        {

        }

        public void StartGame()
        {

        }

    private void OnMouseEnter()
    {
        spriteR.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        spriteR.sprite = initialSprite;
    }
}
