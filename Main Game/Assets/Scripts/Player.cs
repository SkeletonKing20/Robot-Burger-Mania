using UnityEngine;
using System.Collections;
public class Player : Entity, IDamagable {

	public LayerMask punchMe;
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;
	public bool hitConnect;
	

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	Vector3 jabSize;
	float velocityXSmoothing;

	Animator animeThor;
	Controller2D controller;
	CameraFollow camera;
	Vector2 directionalInput;
	void Start() {
		controller = GetComponent<Controller2D> ();
		animeThor = GetComponentInChildren<Animator>();
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		jabSize = new Vector3(2.5f, 0.5f, 0);
		currentHp = maxHp;
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
		if(canAttack())
        {

			lightAttack();
        }
	}

	public bool canAttack()
	{
		float coolDown = 0f;
		if (Time.time > coolDown)
		{
			coolDown = Time.time + 0.5f;
			return true;
		}
        else
        {
			return false;
        }
	}
	public void OnMouseRightDown()
	{

	}
	public void OnSInputUp()
	{
		//Reset Camera Y Position
		camera.verticalOffset = 0.5f;
	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

	public void lightAttack()
    {
		if(controller.collisions.below)
        {
			animeThor.Play("Base Layer.lightAttack", 0, 0);
        }
	}

	public void OnDownTilt()
    {
		downTilt();
    }

	public void downTilt()
    {

    }

	public override void gameOver()
    {

    }
    private void OnDrawGizmos()
    {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(Vector3.right + (0.5f * Vector3.up), jabSize);
	}
}
