using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
	Player player;
    Vector2[] cameraPoints;
    Vector3 targetPosition = new Vector3(-13, 1, -10);
    public bool locked;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void LateUpdate()
    {
        if(player.transform.position.x > -15)
        {
            locked = true;
            Vector3.Lerp(transform.position, targetPosition, 2f);
        }
    }
}
