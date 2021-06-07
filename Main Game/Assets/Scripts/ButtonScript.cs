using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonScript : MonoBehaviour
{
    SpriteRenderer spriteR;
    [SerializeField]
    Sprite hoverSprite;
    Sprite initialSprite;
    [SerializeField]
    buttonActions button;

    bool mouseOver;
    GameHandlerScript gameHandler;
    Player player;
    private void Start()
    {
        gameHandler = FindObjectOfType<GameHandlerScript>();
        player = FindObjectOfType<Player>();
        spriteR = GetComponent<SpriteRenderer>();
        initialSprite = spriteR.sprite;
    }
    private void Update()
    {
        if(mouseOver && Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (button)
            {
                case buttonActions.RESTART:
                    Restart();
                    break;
                case buttonActions.MAIN_MENU:
                    MainMenu();
                    break;
                case buttonActions.QUIT:
                    Quit();
                    break;
                case buttonActions.START_GAME:
                    StartGame();
                    break;
                default:
                    Debug.Log("This is not a Button!");
                    break;
            }
        }
    }
    public void Restart()
    {
        player.OnSceneLoaded();
        gameHandler.isRunning = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        player.OnSceneLoaded();
        Destroy(player.gameObject);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        spriteR.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        spriteR.sprite = initialSprite;
    }
    enum buttonActions
    {
        RESTART = 0,
        MAIN_MENU = 1,
        QUIT = 2,
        START_GAME = 3,
    }
}

