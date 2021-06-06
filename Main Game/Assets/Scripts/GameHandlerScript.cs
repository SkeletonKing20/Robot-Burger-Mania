using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameHandlerScript : MonoBehaviour
{
    Player player;
    public bool isRunning;
    private void Awake()
    {
        isRunning = true;
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.transform.position = new Vector3(-14, -1, 0);
        }
    }
    public void ResetGame()
    {
        isRunning = true;
        player.currentHp = 10;
        player.GetComponentInChildren<Animator>().SetTrigger("Idle");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.currentHp = player.maxHp;
        player.GetComponentInChildren<SpriteRenderer>().transform.localScale = player.scaleRight;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            ResetGame();
        }
    }
}
