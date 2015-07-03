using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {

	public GameObject[] props;
	public GameObject parent;
	public Vector2 position = new Vector2 (0f, 0f); // Y position at spawn
	public Vector2 time = new Vector2(100,200); // centiseconds
	public Vector2 speed = new Vector2(400,400); //centiseconds 
	
	private float next = 0;
	private float sizeOfBoard = 19;
	
	
	// Update is called once per frame
	void Update () {
		next -= Time.deltaTime;
		if (next < 0) { // Spawn a new fish
			Vector3 pos = new Vector3(0,Random.Range(position.x,position.y),0);
			GameObject instance = Instantiate(props[Random.Range(0,props.Length)],pos, Quaternion.identity) as GameObject;
			float x = sizeOfBoard/2 + instance.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
			instance.transform.Translate(new Vector3(x,0,0));
			float s = Random.Range(speed.x, speed.y)/100f;
			instance.GetComponent<AutoMoveCollisionable>().setSpeed(
				new Vector2(-s,0f)
				);
			instance.transform.SetParent(parent.transform);
			next = Random.Range(time.x, time.y)/100;
			Destroy(instance,(instance.GetComponent<SpriteRenderer>().bounds.size.x + sizeOfBoard)/s +2	);
		}
	}
}
