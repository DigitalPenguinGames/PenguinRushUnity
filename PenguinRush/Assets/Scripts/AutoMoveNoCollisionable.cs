using UnityEngine;
using System.Collections;

public class AutoMoveNoCollisionable : MonoBehaviour {
	
	public Vector2 speed = new Vector2(0,0);
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = Time.deltaTime * speed;
		transform.Translate(new Vector3(movement.x,movement.y,0));
	}

	public void setSpeed(Vector2 s) {
		speed = s;
	}
}
