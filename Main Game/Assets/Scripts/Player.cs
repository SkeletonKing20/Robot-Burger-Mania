using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public LayerMask punchMe;
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	Vector3 jabSize;
	float velocityXSmoothing;

	Controller2D controller;
	CameraFollow camera;
	Vector2 directionalInput;

	void Start() {
		controller = GetComponent<Controller2D> ();
		camera = FindObjectOfType<CameraFollow>().GetComponent<CameraFollow>();
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		jabSize = new Vector3(2.5f, 0.5f, 0);
	}

	void Update() {
		CalculateVelocity ();

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	public void OnMouseLeftDown()
    {
		Collider2D otherCollider = Physics2D.OverlapBox(transform.position + Vector3.right, jabSize, 0, punchMe);
		if(otherCollider != null)
        {
			otherCollider?.gameObject.GetComponent<punchingBag>().TakeAHit();
			Debug.Log(otherCollider.gameObject);
		}
	}
	public void OnMouseRightDown()
	{

	}

	public void OnSInputDown()
    {
		camera.verticalOffset = -2;
    }
	public void OnSInputUp()
	{
		//Reset Camera Y Position
		camera.verticalOffset = 1;
	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

    private void OnDrawGizmos()
    {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(Vector3.right, jabSize);
	}
}
