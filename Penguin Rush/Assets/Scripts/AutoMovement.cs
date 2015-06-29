using UnityEngine;
using System.Collections;

public class AutoMovement : MonoBehaviour {

	public float speed = 0;
	public int size = 19;

	private float pos = 0f;
	
	// Update is called once per frame
	void Update () {
		float movement = -speed*Time.deltaTime;
		pos += movement;
		if (transform.position.x < -size) {
			pos += 2*size;
			transform.Translate(new Vector3(2*size,0,0));
		}
		transform.Translate(new Vector3(movement,0,0));
	}
}
