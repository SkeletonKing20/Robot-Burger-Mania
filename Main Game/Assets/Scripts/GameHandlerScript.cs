using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameHandlerScript : MonoBehaviour
{
    Player player;
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
