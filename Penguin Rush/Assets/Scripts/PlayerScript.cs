using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float jumptime = 0.4f;
	public float dist = 4.5f;
	public Vector2 center = new Vector2(0,-0.15f);

	private Vector2 movement = new Vector2(0,0);
	private float gravity = 0f;

	private enum dir {
		up, down, none
	};
	private dir lastJump = dir.none;

	void Awake() {
		transform.position = center;
	}

	// Update is called once per frame
	void Update () {
		if (lastJump == dir.none) {
			int inputY = 0;
			if (Input.GetAxis("Vertical") > 0) {
				inputY = 1;
				lastJump = dir.up;
			}
			else if (Input.GetAxis("Vertical") < 0) {
				inputY = -1;
				lastJump = dir.down;
			}
			if (inputY != 0) {
				float factor = 2f/(jumptime * jumptime);
				gravity = - factor * ((dist + center.y) - transform.position.y);
				movement.y = - gravity * jumptime;
				movement.y *= inputY;
			}
			//print(inputY);
		}

	}

	void FixedUpdate() {
		// Gravity
		if (transform.position.y > center.y) movement.y += gravity*Time.deltaTime;
		else movement.y -= gravity*Time.deltaTime;
		if (transform.position.y > center.y && transform.position.y + movement.y*Time.deltaTime < center.y
		    || transform.position.y < center.y && transform.position.y + movement.y*Time.deltaTime > center.y) {
			movement.y *= 0.5f;
			lastJump = dir.none;
		}
		GetComponent<Rigidbody2D>().velocity = movement;	

		//print("gravity " + gravity  + " movement: " + movement.y);
	}
}
