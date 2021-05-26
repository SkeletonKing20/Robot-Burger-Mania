using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Controller2D target;
	public float lookSmoothTime;
	bool locked;
	private Vector3 smoothLookVelocity;

	void Start() 
	{
		lookSmoothTime = 0.2f;
		smoothLookVelocity = Vector3.zero;
	}

	void LateUpdate() {
        if (!locked)
        {
			Vector3 targetPosition;
			targetPosition.x = target.transform.position.x + 5f;
			targetPosition.y = target.transform.position.y + 0.95f;
			targetPosition.z = -10;

			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothLookVelocity, lookSmoothTime);
		}
	}
}
