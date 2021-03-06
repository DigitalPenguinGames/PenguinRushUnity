using UnityEngine;
using XInputDotNetPure; // Required in C#

public class PlayerScript : MonoBehaviour {

	public GameObject scripts;

	public float jumptime = 0.4f;
	public float dist = 4.5f;
	public Vector2 center = new Vector2(-7,-0.15f);

	//public float rotationForce = 1;

	private Vector2 movement = new Vector2(0,0);
	private float gravity = -25f;

	private enum dir {
		up, down, none
	};

	private bool cutGravity = false;
	private dir lastJump = dir.none;
	private float lastRotation = 0;

	private float obstacleSpeed = 6;

	private bool canDie = false;

	private bool collisioning = false;

	#if UNITY_STANDALONE || UNITY_WEBPLAYER
	private string up;
	private string down;
	// Rumble Thing
	private bool playerIndexSet = false;
	private PlayerIndex playerIndex;
	private GamePadState state;
	private float rumbleCount = 0;
	const float rumbleTime = 0.2f;
	#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
	private bool touchTypeHorizontal;
	#endif
	// Tracking things
	private int numberOfJumps;



	void Awake() {
		transform.position = new Vector2(center.x - 3 , center.y);
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		up = PlayerPrefs.GetString("keyUp","up");
		down = PlayerPrefs.GetString("keyDown","down");
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		touchTypeHorizontal = PlayerPrefs.GetInt("touchTypeHorizontal",1) == 1;
		#endif
		// Tracking things
		numberOfJumps = PlayerPrefs.GetInt("trackJumps",0);
	}

	void OnDestroy() {
		if (scripts != null) scripts.AddComponent<EndGameScript>();
	}

	// Update is called once per frame
	void Update () {

		//Rumble Thing
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		if (!playerIndexSet || !state.IsConnected) getPlayerIndex();
		if (state.IsConnected) {
			rumbleCount -= Time.deltaTime;
			if (rumbleCount < 0) GamePad.SetVibration(playerIndex,0,0);
		}
		#endif
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
			if (Input.GetKey(up)) verticalInput = 1;
			else if (Input.GetKey(down)) verticalInput = -1;
			//verticalInput = Input.GetAxis("Vertical");
			else if (Input.GetButton("Fire1")) {
				verticalInput = 1;
			}
			else if (Input.GetButton("Fire2")) {
				verticalInput = -1;
			}
			#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			if(Input.touchCount > 0) {
				Touch myTouch = Input.touches[0];
				
				if(myTouch.phase == TouchPhase.Began){
					float position = myTouch.position.y;
					if(touchTypeHorizontal) {
						if (position >= Screen.width/2) verticalInput = 1;
						else verticalInput = -1;
					}
					else {
						if (position <= Screen.height/2) verticalInput = 1;
						else verticalInput = -1;
					}
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
				// xf = 1/2 a * tº2 + v*t + xi    xi -> centre + dist   xf -> centre.y +- pos.y
				// a = (xf - xi)* factor    factor = 1/ (1/2*t_2)
				float factor = 2f/(jumptime * jumptime); //inversa de   1/2 * temps al quadrat
				gravity = factor * (transform.position.y - (center.y + inputY*dist));
				movement.y = - gravity * jumptime;
				GetComponent<Rigidbody2D>().velocity = movement;
				gravity *= inputY;
				cutGravity = false;

				// Tracking things
				++numberOfJumps;
				PlayerPrefs.SetInt("trackJumps",numberOfJumps);

				#if UNITY_STANDALONE || UNITY_WEBPLAYER
				//Rumble Thing
				state = GamePad.GetState(playerIndex);
				if (state.IsConnected) {
					GamePad.SetVibration(playerIndex,1,1);
					rumbleCount = rumbleTime;
				}
				#endif	
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

		if (transform.position.y > center.y && lastJump == dir.down && movement.y > 0) { // Too high
			Debug.LogWarning("The penguin was going up");
			Debug.LogWarning("y: " + transform.position.y + " dir: " + lastJump + " speed: " + movement.y);
			lastJump = dir.up;
		}
		else if (transform.position.y < center.y && lastJump == dir.up && movement.y < 0) { // Too low
			Debug.LogWarning("The penguin was going down");
			Debug.LogWarning("y: " + transform.position.y + " dir: " + lastJump + " speed: " + movement.y);
			lastJump = dir.down;
		}

	}

	void FixedUpdate() {
		movement = GetComponent<Rigidbody2D>().velocity;

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

		// Horizontal Speed
		if (!collisioning) movement.x = center.x - transform.position.x;
		collisioning = false;
		// Speed
		GetComponent<Rigidbody2D>().velocity = movement;

		// Agular
		/*float angularDirection;
		angularDirection = transform.rotation.z;
		if (transform.rotation.z > 0) angularDirection = Mathf.Max (0.2f, Mathf.Min(0.6f ,angularDirection));
		else angularDirection = Mathf.Min (-0.2f, Mathf.Max( -0.6f, angularDirection));
		angularDirection = - Mathf.Pow(angularDirection*100,2) * Mathf.Sign(angularDirection);
		*/

		// jump Rotation
		float distance = Mathf.Abs(transform.position.y - center.y);
		float jumpRotation = 360 + 180 - Mathf.Rad2Deg * Mathf.Atan2(movement.y,(movement.x-obstacleSpeed));
		float diff = 0;
		if(distance < 0.05 && (((jumpRotation%360) < 10) || ((jumpRotation%360) > 350))){
			jumpRotation = 0;
			if (!collisioning) GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
		else {
			float aux = jumpRotation % 360;

			//Debug.Log("Diff " +diff);
			if (lastRotation > 180 && aux < 180) {
				//Debug.Log("aux " + aux + " last " + lastRotation + " diff " + (360 - lastRotation + aux));
				diff = 360 - lastRotation + aux;
			}
			else if (lastRotation < 180 && aux > 180 ) {
				//Debug.Log("aux " + aux + " last " + lastRotation + " diff " + (-360 - lastRotation + aux));
				diff = -360 - lastRotation + aux;
			}
			else { 
				//Debug.Log("aux " + aux + " last " + lastRotation + " diff " + (aux - lastRotation));
				diff = aux - lastRotation;
			}

			diff = Mathf.Min(10,Mathf.Max(-10,diff));
		}
		//if (diff != 0) lastRotation = (lastRotation + diff) % 360; 
		//else 
		lastRotation = (jumpRotation) % 360;
		if (!collisioning) transform.rotation = Quaternion.Euler(new Vector3(0,0,lastRotation));
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Goal") {
			if (other.name == "goal3(Clone)") scripts.GetComponent<NextLevelScript>().nextLevel();
			Destroy(other.gameObject, 0.1f);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Mix" || 
		    other.gameObject.tag == "Down" ||
		    other.gameObject.tag == "Up") collisioning = true;
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Mix" || 
		    other.gameObject.tag == "Down" ||
		    other.gameObject.tag == "Up") collisioning = true;
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

	#if UNITY_STANDALONE || UNITY_WEBPLAYER
	// Rumble Thing
	void getPlayerIndex () {
		for (int i = 0; i < 4; ++i)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)i;
			GamePadState testState = GamePad.GetState(testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
				playerIndex = testPlayerIndex;
				playerIndexSet = true;
			}
		}
	}
	#endif

}
