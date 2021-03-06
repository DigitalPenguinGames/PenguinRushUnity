﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public float factor = 1;
	public Text score;
	public Text highscoreT;

	private float time = 0;
	private bool run = false;
	private float highscore = 0;
	private bool visible;

	// Tracking things
	private float trackingTotalTime;
	private float trackingTotalScore;

	// Goal
	private bool firstGoalB = false;
	private bool secondGoalB = false;
	private bool thirdGoalB = false;
	public int firstGoalS;
	public int secondGoalS;
	public int thirdGoalS;

	// Languaje
	Lang lang;

	void Start () {
		// Languaje
		lang = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LangManager>().lang;

		if (PlayerPrefs.GetInt ("endlessMode", 0) == 1) {
			thirdGoalS *= 100;
			PlayerPrefs.DeleteKey("endlessMode");
		}

		highscore = PlayerPrefs.GetFloat("HighScore");
		visible = PlayerPrefs.GetInt("showScores",1) == 1;
		score.text = lang.getString("stage_score") + " : 0";
		highscoreT.text = lang.getString("stage_high_score") + " : " + highscore.ToString("F0");
		if (!visible) {
			enableTexFields(false);
		}
		// Tracking things
		trackingTotalTime = PlayerPrefs.GetFloat("trackTotalTime",0);
		trackingTotalScore = PlayerPrefs.GetFloat("trackTotalScore",0);

	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			time += Time.deltaTime;
			score.text = lang.getString("stage_score") +" : " + (time * factor).ToString("F0");
			if (time*factor > highscore) {
				highscore = time*factor;
				highscoreT.text = lang.getString("stage_high_score") + " : " + highscore.ToString("F0");
				PlayerPrefs.SetFloat("HighScore",highscore);
			}
			if (time*factor > firstGoalS && !firstGoalB) {
				GetComponentInParent<ObstacleSpawner>().spawnGoal(1);
				firstGoalB = true;
			}
			if (time*factor > secondGoalS && !secondGoalB) {
				GetComponentInParent<ObstacleSpawner>().spawnGoal(2);
				secondGoalB = true;
			}
			if (time*factor > thirdGoalS && !thirdGoalB) {
				GetComponentInParent<ObstacleSpawner>().spawnGoal(3);
				thirdGoalB = true;
			}

			// Tracking things
			trackingTotalTime += Time.deltaTime;
			trackingTotalScore += Time.deltaTime * factor;
			PlayerPrefs.SetFloat("trackTotalTime", trackingTotalTime);
			PlayerPrefs.SetFloat("trackTotalScore", trackingTotalScore);
		}
	}

	public void stop() {
		run = false;
	}

	public void startScore() {
		run = true;
		enableTexFields(visible);
	}

	public void resetScore() {
		time = 0;
		// Pensar en si tienen qe volver a aparecer o no
		firstGoalB = false;
		secondGoalB = false;
		thirdGoalB = false;

	}

	public void enableTexFields(bool b) {
		score.enabled = b;
		highscoreT.enabled = b;
	}
}
