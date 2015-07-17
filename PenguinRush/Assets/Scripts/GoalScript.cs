using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	public GameObject scripts;

	public float jumptime = 0.4f;
	public float dist = 4.5f;
	public Vector2 center = new Vector2(0,-0.15f);

	private Vector2 movement = new Vector2(-1f,0);
	private float gravity = -25f;

	private enum dir {
		up, down, none
	};
	
	private bool cutGravity = false;
	private dir lastJump = dir.none;

	private float obstacleSpeed = 6;
	private GameObject obstacle;

	void OnDestroy() {
		//if (scripts != null) scripts.AddComponent<EndGameScript>(); // Pass level or continue?
	}

	void Update() {
		if (transform.position.x + GetComponent<Renderer>().bounds.size.x/2 <  -19f/2) {
			Destroy(gameObject);
		}
		// jump!
		if (obstacle != null && timeToCollision() < jumptime) {
			int inputY = 0;

			if (obstacle.tag == "Up") {
				inputY = 1;
				lastJump = dir.up;
			}
			else if (obstacle.tag == "Down") {
				inputY = -1;
				lastJump = dir.down;
			}
			else {
				inputY = Random.Range(0,1);
				if (inputY == 1) lastJump = dir.up;
				else {
					inputY = -1;
					lastJump = dir.down;
				}
			}
			if (inputY != 0) { 
				// xf = 1/2 a * tº2 + v*t + xi    xi -> centre + dist   xf -> centre.y +- pos.y
				// a = (xf - xi)* factor    factor = 1/ (1/2*t_2)
				float factor = 2f/(jumptime * jumptime); //inversa de   1/2 * temps al quadrat
				gravity = factor * (transform.position.y - (center.y + inputY*dist));
				movement.y = - gravity * jumptime;	
				gravity *= inputY;
				cutGravity = false;
				obstacle = null;
			}
		}
	}

	void FixedUpdate() {
		
		// Gravity
		if(lastJump == dir.up) movement.y += gravity*Time.fixedDeltaTime;
		else if(lastJump == dir.down) movement.y -= gravity*Time.fixedDeltaTime;
		else if (transform.position.y > center.y) movement.y += gravity*Time.fixedDeltaTime;
		else movement.y -= gravity*Time.fixedDeltaTime;
		
		// Crossing the middle
		if (transform.position.y > center.y && transform.position.y + movement.y*Time.deltaTime < center.y && lastJump == dir.up
		    || transform.position.y < center.y && transform.position.y + movement.y*Time.deltaTime > center.y && lastJump == dir.down) {
			lastJump = dir.none;
			cutGravity = true;
		}
		if (transform.position.y > center.y && transform.position.y + movement.y*Time.deltaTime < center.y && cutGravity
		    || transform.position.y < center.y && transform.position.y + movement.y*Time.deltaTime > center.y && cutGravity) {
			movement.y *= 0.6f;
		}

		// Speed
		GetComponent<Rigidbody2D>().velocity = movement;
		
		
		
		// jump Rotation
		float distance = Mathf.Abs(center.y - transform.position.y);
		float jumpRotation = 360 + 180 - Mathf.Rad2Deg * Mathf.Atan2(movement.y,(movement.x-obstacleSpeed));
		if(distance < 0.05 && (((jumpRotation%360) < 10) || ((jumpRotation%360) > 350))){
			jumpRotation = 0;
		}
		transform.rotation = Quaternion.Euler(new Vector3(0,0,jumpRotation));
		//print(jumpRotation+" ------------- "+rotation);
	}

	public void newObstacle(GameObject obs, float speed) {
		obstacle = obs;
		obstacleSpeed = speed;
	}

	private float timeToCollision() {
		float obsPos = obstacle.transform.position.x;
		float goalPos = transform.position.x;
		if (goalPos < 0) {
			obsPos -= goalPos;
			goalPos = 0;
		}
		float dist = obsPos - goalPos;
		return dist / obstacleSpeed;
	}
}
