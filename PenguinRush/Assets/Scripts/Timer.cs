using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public float factor = 1;
	public Text score;

	private float time = 0;
	private bool run = true;

	// Use this for initialization
	void Start () {
		score.text = "Score : 0";
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			time += Time.deltaTime;
			score.text = "Score : " + (time * factor).ToString("F0");
		}
	}

	public void stop() {
		run = false;
	}
}
