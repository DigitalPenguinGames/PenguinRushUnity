using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public float factor = 1;
	public Text score;
	public Text highscoreT;

	private float time = 0;
	private bool run = false;
	private float highscore = 0;

	// Use this for initialization
	void Start () {
		score.text = "Score : 0";
		highscore = PlayerPrefs.GetFloat("HighScore");
		highscoreT.text = "High Score : " + highscore.ToString("F0");
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			time += Time.deltaTime;
			score.text = "Score : " + (time * factor).ToString("F0");
			if (time*factor > highscore) {
				highscore = time*factor;
				highscoreT.text = "Highscore : " + highscore.ToString("F0");
			}
		}
	}

	public void stop() {
		run = false;
	}

	public void startScore() {
		run = true;
	}

	public void resetScore() {
		time = 0;
	}

	public void saveScore() {
		PlayerPrefs.SetFloat("HighScore",highscore);
	}
}
