using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Controller2D target;
	public float lookSmoothTime;
	bool locked;
	private Vector3 smoothLookVelocity;
	Vector3 targetPosition;
	void Start() 
	{
		lookSmoothTime = 0.1f;
		smoothLookVelocity = Vector3.zero;
		transform.position = new Vector3(0,target.transform.position.y + 3.802911f,0);
	}

	void Update() {
        if (!locked)
        {

			targetPosition.x = target.transform.position.x + 5f;

			targetPosition.z = -10;

			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothLookVelocity, lookSmoothTime);
		}
	}
}
