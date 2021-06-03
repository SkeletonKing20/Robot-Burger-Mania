using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Controller2D target;
	public float lookSmoothTime;
	bool locked;
	private Vector3 smoothLookVelocity;
	Vector3 lastCameraPosition;
	Vector3 targetPosition;
	void Start() 
	{
		lookSmoothTime = 0.1f;
		smoothLookVelocity = Vector3.zero;
		transform.position = new Vector3(0,1,-10);
		lastCameraPosition = transform.position;
		target = FindObjectOfType<Player>().GetComponent<Controller2D>();
	}

	void Update() {
		if (transform.position.x >= 0 && !locked)
		{
			followPlayer();
		}
		if (transform.position.x < 0)
		{
			transform.position = new Vector3(0, 1, -10);
		}
	}

	public void followPlayer()
    {
		targetPosition.x = target.transform.position.x + 5f;

		targetPosition.z = -10;

		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, 1, -10), ref smoothLookVelocity, lookSmoothTime);
	}
}
