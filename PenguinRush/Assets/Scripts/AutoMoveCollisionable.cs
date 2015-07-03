using UnityEngine;
using System.Collections;

public class AutoMoveCollisionable : MonoBehaviour {

	private Vector2 speed = new Vector2(0,0);
	private float center = -0.15f;
	private float gravity = 0.2f;

	void Start() {
		GetComponent<Rigidbody2D>().velocity = speed;
	}

	void FixedUpdate() {
		float gravityDirection;
		if (transform.position.y > center) gravityDirection = -1;
		else gravityDirection = 1;
		GetComponent<Rigidbody2D>().velocity += new Vector2(0, gravity * gravityDirection);
	}

	public void setSpeed(Vector2 s) {
		speed = s;
	}
}
