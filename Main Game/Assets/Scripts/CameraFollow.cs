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
		target = FindObjectOfType<Player>().GetComponent<Controller2D>();
		if (transform.position.x >= 0 && transform.position.x <= 115)
		{
			followPlayer();
		}
		if (transform.position.x < 0)
		{
			transform.position = new Vector3(0, 1, -10);
		}
		if (transform.position.x > 115)
		{
			transform.position = new Vector3(115, 1, -10);
		}
	}

	public void followPlayer()
    {
		targetPosition.x = target.transform.position.x + 5f;

		targetPosition.z = -10;

		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, 1, -10), ref smoothLookVelocity, lookSmoothTime);
	}
}
