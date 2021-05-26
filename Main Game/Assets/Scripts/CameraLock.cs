using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
	Player player;
    public Vector3[] cameraPoints;
    public bool locked;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        foreach(Vector3 position in cameraPoints)
        {

        }
    }
}
