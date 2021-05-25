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
			targetPosition.x = target.transform.position.x + 8f;
			targetPosition.y = target.transform.position.y + 0.95f;
			targetPosition.z = -10;

			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothLookVelocity, lookSmoothTime);
		}
	}


	struct FocusArea {
		public Vector2 centre;
		public Vector2 velocity;
		float left,right;
		float top,bottom;


		public FocusArea(Bounds targetBounds, Vector2 size) {
			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			centre = new Vector2((left+right)/2,(top +bottom)/2);
		}

		public void Update(Bounds targetBounds) {
			float shiftX = 0;
			if (targetBounds.min.x < left) {
				shiftX = targetBounds.min.x - left;
			} else if (targetBounds.max.x > right) {
				shiftX = targetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if (targetBounds.min.y < bottom) {
				shiftY = targetBounds.min.y - bottom;
			} else if (targetBounds.max.y > top) {
				shiftY = targetBounds.max.y - top;
			}
			top += shiftY;
			bottom += shiftY;
			centre = new Vector2((left+right)/2,(top +bottom)/2);
			velocity = new Vector2 (shiftX, shiftY);
		}
	}

}
