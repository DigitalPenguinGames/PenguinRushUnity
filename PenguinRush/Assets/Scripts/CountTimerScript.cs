using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountTimerScript : MonoBehaviour {

	public float seconds = 3;
	public float numbers = 3;

	private float elapse;
	private float timer;
	public Text ttimer;

	private bool running = true;

	private GameObject instance;

	void Start () {
		restart();
	}
	
	// Update is called once per frame
	void Update () {
		if (!running) return;
		elapse -= Time.deltaTime;
		timer = elapse*(numbers/seconds);
		if (timer < -1) {
			ttimer.text = "";
			startThings();
		}
		else if (timer < 0) ttimer.text = "GO!";
		else ttimer.text = Mathf.Floor(0.99f+timer).ToString("F0");
	}

	void startThings() {
		GetComponentInParent<Score>().startScore();
		GetComponentInParent<ObstacleManager>().start();
		running = false;
		instance.GetComponent<PolygonCollider2D>().enabled = true;
		instance.GetComponent<PlayerScript>().setCanDie(true);
		instance = null;
	}

	public void restart() {
		spawnPlayer();
		GetComponentInParent<ObstacleManager>().stop();
		if (GetComponentInParent<EndGameScript>() != null) Destroy(GetComponentInParent<EndGameScript>());
		timer = numbers;
		ttimer.text = (0.9f+timer).ToString("F0");
		elapse = seconds;
		running = true;
	}

	void spawnPlayer() {
		instance = Instantiate(Resources.Load("player"), new Vector3(0,0,0), Quaternion.identity) as GameObject;
		instance.transform.SetParent(GameObject.FindWithTag("Middleground").transform);
		instance.GetComponent<PlayerScript>().setCanDie(false);
		instance.tag = "Player";
		instance.GetComponent<PlayerScript>().scripts = gameObject;
		instance.GetComponent<PolygonCollider2D>().enabled = false;
		GetComponentInParent<ObstacleManager>().player = instance;
	}
}
