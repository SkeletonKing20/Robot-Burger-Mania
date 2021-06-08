using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalingProblem : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.scaleProperly();
    }
}
