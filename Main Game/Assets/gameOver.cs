using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    private void Update()
    {
        
    }

    public void gameOverTriggered()
    {
        this.gameObject.SetActive(true);
    }
}
