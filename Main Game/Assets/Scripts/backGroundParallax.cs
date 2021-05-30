using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundParallax : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private Vector3 deltaMovement;
    [SerializeField]
    private Vector2 parallaxMultiplier;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = transform.position;
    }

    private void LateUpdate()
    {
        deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y);
        lastCameraPosition = transform.position;
    }
}
