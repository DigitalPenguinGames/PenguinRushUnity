using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float jumptime = 1;
	public float dist = 4;

	//public Vector2 speed = new Vector2(50,0);
	
	public float gravity = 0f;
	public float center = -0.15f;
	public float interval = 0.5f;

	private Vector2 movement = new Vector2(0,0);
	private bool jumping = false;
//	private bool directionUp;

	// Update is called once per frame
	void Update () {
		if (transform.position.y < center + interval && transform.position.y > center - interval) {
			jumping = false;
		}
		else jumping = true;
		if (!jumping) {
			int inputY = 0;
			if (Input.GetAxis("Vertical") > 0) inputY = 1;
			else if (Input.GetAxis("Vertical") < 0) inputY = -1;
			if (inputY != 0) {
				float factor = 2f/(jumptime * jumptime);
				gravity = - factor * ((dist + center) - transform.position.y);
				movement.y = - gravity * jumptime;
				movement.y *= inputY;
				jumping = true;
			}
			else jumping = false;
			print(inputY);
		}
		// Gravity

	}

	void FixedUpdate() {
		if (transform.position.y > center) movement.y += gravity*Time.deltaTime;
		else movement.y -= gravity*Time.deltaTime;
		if (transform.position.y > center && transform.position.y + movement.y*Time.deltaTime < center
		    || transform.position.y < center && transform.position.y + movement.y*Time.deltaTime > center) 
			movement.y *= 0.5f;
		GetComponent<Rigidbody2D>().velocity = movement;	
		print("gravity " + gravity  + " movement: " + movement.y);
	}
}
