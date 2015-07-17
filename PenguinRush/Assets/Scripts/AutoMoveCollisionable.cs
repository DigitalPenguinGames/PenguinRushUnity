using UnityEngine;
using System.Collections;

public class AutoMoveCollisionable : MonoBehaviour {

	private Vector2 speed = new Vector2(0,0);
	private float center = -0.15f;
	private float gravity = 0.1f;
	private float angularForce = 1f;
	private float initRotation = 0f;

	void Start() {
		GetComponent<Rigidbody2D>().velocity = speed;
		transform.Rotate(new Vector3(0f,0f,initRotation));
	}

	void FixedUpdate() {
		float gravityDirection;
		if (transform.position.y > center) gravityDirection = -1;
		else gravityDirection = 1;
		GetComponent<Rigidbody2D>().velocity += new Vector2(0, gravity * gravityDirection);
		float rotation = transform.rotation.z;
		float angularDirection = -1;
		if ( rotation < 0) angularDirection = 1;
		GetComponent<Rigidbody2D>().angularVelocity += angularForce*angularDirection;
	}

	public void setSpeed(Vector2 s) {
		speed = s;
	}

	public void setRotation(float r) {
		initRotation = r;
	}
	
}
