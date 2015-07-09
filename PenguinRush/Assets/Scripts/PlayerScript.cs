using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public GameObject scripts;

	public float jumptime = 0.4f;
	public float dist = 4.5f;
	public Vector2 center = new Vector2(-7,-0.15f);

	public float rotationForce = 1;

	private Vector2 movement = new Vector2(0,0);
	private float gravity = -25f;

	private enum dir {
		up, down, none
	};
	private dir lastJump = dir.none;

	private float obstacleSpeed=0;

	private bool canDie = false;

	void Awake() {
		transform.position = new Vector2(center.x - 3 , center.y);
	}

	void OnDestroy() {
		scripts.AddComponent<EndGameScript>();
	}

	// Update is called once per frame
	void Update () {
		// Is dead?
		//print("trnas " + transform.position.x + " bounds " + GetComponent<Renderer>().bounds.size.x + " screen " + Screen.width);
		if (canDie && Mathf.Abs( transform.position.x) - GetComponent<Renderer>().bounds.size.x/2 >  19f/2) {
			Destroy(gameObject);
		}

		// jump!
		if (lastJump == dir.none) {
			int inputY = 0;
			float verticalInput = 0.0f;

			#if UNITY_STANDALONE || UNITY_WEBPLAYER
			verticalInput = Input.GetAxis("Vertical");
			if (Input.GetButton("Fire1")) {
				verticalInput = 1;
			}
			if (Input.GetButton("Fire2")) {
				verticalInput = -1;
			}
			#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			if(Input.touchCount > 0) {
				Touch myTouch = Input.touches[0];
				
				if(myTouch.TouchPhase == TouchPhase.Began){
					float position = myTouch.position.y;
					if(position >= Screen.height/2) verticalInput = 1;
					else verticalInput = -1;
				}
			}
			#endif

			if (verticalInput > 0) {
				inputY = 1;
				lastJump = dir.up;
			}
			else if (verticalInput < 0) {
				inputY = -1;
				lastJump = dir.down;
			}
			if (inputY != 0) {
				float factor = 2f/(jumptime * jumptime);
				gravity = - factor * ((dist + center.y) - transform.position.y);
				movement.y = - gravity * jumptime;
				movement.y *= inputY;
			}
		}

		//PARTICLES STUFFS
		if(   transform.position.y > center.y+0.8 && movement.y < 0
		   || transform.position.y < center.y-0.8 && movement.y > 0 ){
			GetComponentInChildren<ParticleSystem>().emissionRate = 0;
		}
		else {
			GetComponentInChildren<ParticleSystem>().emissionRate = 80;
		}
	}

	void FixedUpdate() {
		// Gravity
		if (transform.position.y > center.y) movement.y += gravity*Time.deltaTime;
		else movement.y -= gravity*Time.deltaTime;
		// Crossing the middle
		if (transform.position.y > center.y && transform.position.y + movement.y*Time.deltaTime < center.y
		    || transform.position.y < center.y && transform.position.y + movement.y*Time.deltaTime > center.y) {
			movement.y *= 0.5f;
			lastJump = dir.none;
		}
		// Horizontal Speed
		movement.x = center.x - transform.position.x;
		// Speed
		GetComponent<Rigidbody2D>().velocity = movement;

		// Agular
		float angularDirection;
		angularDirection = transform.rotation.z;
		if (transform.rotation.z > 0) angularDirection = Mathf.Max (0.2f, Mathf.Min(0.6f ,angularDirection));
		else angularDirection = Mathf.Min (-0.2f, Mathf.Max( -0.6f, angularDirection));
		angularDirection = - Mathf.Pow(angularDirection*100,2) * Mathf.Sign(angularDirection);
		// jump Rotation
		float jumpRotation = 180 - Mathf.Rad2Deg * Mathf.Atan2(movement.y,(movement.x-obstacleSpeed));
		float auxRotation = transform.rotation.eulerAngles.z;
		//jumpRotation = unityRotation(jumpRotation-auxRotation);
		GetComponent<Rigidbody2D>().angularVelocity = 
			(rotationForce * angularDirection * 0.5f) + 
			GetComponent<Rigidbody2D>().angularVelocity*0.5f;
			//+ (jumpRotation - auxRotation) * 20;
		
	}
	
	private float unityRotation(float degrees) {
		if (degrees < 180) return degrees/180;
		else return -(1 - degrees/180);
	}

	public void setObstacleSpeed(float s) {
		obstacleSpeed = s;
	}

	public void setCanDie(bool b) {
		canDie = b;
	}
}
