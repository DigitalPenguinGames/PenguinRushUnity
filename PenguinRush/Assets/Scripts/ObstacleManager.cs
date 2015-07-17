using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

	public GameObject[] Background;
	public GameObject[] props;
	public GameObject parent;
	public GameObject player;
	public GameObject[] goal;

	public Vector2 position = new Vector2 (0f, 0f); // Y position at spawn
	public Vector2 time = new Vector2(100,200); // centiseconds
	public Vector2 speed = new Vector2(400,400); //centiseconds 
	public Vector2 rotation = new Vector2(90,90);
	
	private float next = 0;
	private float sizeOfBoard = 19;
	private float speedFactor = 1;
	private bool finished = false;
	private bool run = false;

	private GameObject instanceGoal;
	private bool spawningGoal = false;
	private int numberOfGoal;
	

	void Start() {
		instanceGoal = null;
	}

	// Update is called once per frame
	void Update () {
		if (!run) return;
		next -= Time.deltaTime;
		if (next < 0) { // Spawn a new fish
			//if (!finished) {
				propagateSpeed(speedFactor + 0.05f);

			//}

			Vector3 pos = new Vector3(0,Random.Range(position.x,position.y),0);
			GameObject instance = Instantiate(props[Random.Range(0,props.Length)],pos, Quaternion.identity) as GameObject;
			float x = sizeOfBoard/2 + instance.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
			instance.transform.Translate(new Vector3(x,0,0));
			float s = speedFactor*Random.Range(speed.x, speed.y)/100f;
			instance.GetComponent<AutoMoveCollisionable>().setSpeed(
				new Vector2(-s,0f)
			);
			float r = Random.Range(rotation.x,rotation.y) - (rotation.x+rotation.y)/2;
			instance.GetComponent<AutoMoveCollisionable>().setRotation(r);
			instance.transform.SetParent(parent.transform);
			next = Random.Range(time.x,time.y)/100;
			Destroy(instance,(instance.GetComponent<SpriteRenderer>().bounds.size.x + sizeOfBoard)/s +2	);
			if ( player != null ) player.GetComponent<PlayerScript>().setObstacleSpeed(s);

			if (spawningGoal) { //speedFactor == Mathf.Floor(speedFactor)) {
				instanceGoal = Instantiate(goal[numberOfGoal-1],pos,Quaternion.identity) as GameObject;
				float auxX = sizeOfBoard/2 + instanceGoal.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
				instanceGoal.transform.Translate(new Vector3(auxX,0,0));
				instanceGoal.transform.SetParent(parent.transform);
				spawningGoal = false;
			}
			if (instanceGoal != null) instanceGoal.GetComponent<GoalScript>().newObstacle(instance,s);
		}
		
	}

	public void propagateSpeed(float speed) {
		speedFactor = speed;
		// speed up the background
		foreach ( GameObject g in Background) 
			g.GetComponent<InfiniteMovement>().setSpeed(speed);

		// speed up the fishes
		gameObject.GetComponent<PropsManager>().setSpeedFactor(speed);
	}

	
	public void start() {
		run = true;
		finished = false;
		speedFactor = 1;
		next = 0;
	}

	public void stop() {
		run = false;
	}

	
	public void setFinished(bool f) {
		finished = f;
	}
	
	public bool getFinished(){
		return finished;
	}

	public float getSpeedFactor() {
		return speedFactor;
	}

	public void spawnGoal(int n) {
		numberOfGoal = n;
		spawningGoal = true;
	}
}
