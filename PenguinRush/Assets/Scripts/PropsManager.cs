using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PropsManager : MonoBehaviour {

	public GameObject[] fish;
	public Vector2 fishPosition = new Vector2 (-1f, -4.5f); // Y position at spawn
	public Vector2 fishTime = new Vector2(100,200);
	public Vector2 fishSpeed = new Vector2(100,300); //centiseconds 
	private float nextFish = 0;
	

	// Update is called once per frame
	void Update () {
		nextFish -= Time.deltaTime;
		if (nextFish < 0) { // Spawn a new fish
			Vector3 pos = new Vector3(10,Random.Range(fishPosition.x,fishPosition.y),0);
			GameObject instance = Instantiate(fish[Random.Range(0,fish.Length)],pos, Quaternion.identity) as GameObject;
			float speed = Random.Range(fishSpeed.x, fishSpeed.y)/100f;
			instance.GetComponent<AutoMoveNoCollisionable>().setSpeed(
				new Vector2(-speed,0f)
			);
			instance.transform.SetParent(GameObject.FindGameObjectWithTag("Foreground").transform);
			nextFish = Random.Range(fishTime.x, fishTime.y);
			Destroy(instance,1 + 19/speed);
		}
	}
}
