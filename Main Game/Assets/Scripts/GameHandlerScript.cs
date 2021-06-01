using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameHandlerScript : MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            ResetGame();
        }
    }
}
