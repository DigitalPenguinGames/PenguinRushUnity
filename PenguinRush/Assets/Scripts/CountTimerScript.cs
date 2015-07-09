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
		GameObject.FindWithTag("Player").GetComponent<PlayerScript>().setCanDie(true);
		GameObject.FindWithTag("Player").GetComponent<PolygonCollider2D>().enabled = true;
	}

	public void restart() {
		GetComponentInParent<ObstacleManager>().stop();
		if (GetComponentInParent<EndGameScript>() != null) Destroy(GetComponentInParent<EndGameScript>());
		timer = numbers;
		ttimer.text = (0.9f+timer).ToString("F0");
		elapse = seconds;
		running = true;
	}
}
