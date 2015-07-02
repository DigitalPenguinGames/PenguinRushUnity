using UnityEngine;
using System.Collections;

public class AutoMoveCollisionable : MonoBehaviour {

	public Vector2 speed = new Vector2(0,0);


	// Update is called once per frame
	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = speed;
	}
}
