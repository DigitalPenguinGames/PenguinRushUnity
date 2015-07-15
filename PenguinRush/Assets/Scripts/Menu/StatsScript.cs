﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour {

	//private int numberOfTries;
	private Text stats;	

	// Use this for initialization
	void Start () {
		int Tries = PlayerPrefs.GetInt("trackTries",0);
		int Jumps = PlayerPrefs.GetInt("trackJumps",0);
		stats = GameObject.Find("Stats").GetComponent<Text>();
		stats.text = "Number of \n" +
			"Tries: " + Tries + "\n" +
			"Jumps: " + Jumps + "\n"
			;
	}

	void OnDestroy() {
		if (stats != null) stats.text = "";
	}
	
	void OnGUI() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		GUI.skin.button.fontSize = Screen.width/90;
		GUI.skin.textArea.fontSize = Screen.width/90;
		int buttonWidth = Screen.width/9;
		int buttonHeight = buttonWidth/4;
		int offset = Mathf.FloorToInt(buttonHeight*1.6f);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		GUI.skin.button.fontSize = Screen.width/40;
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		float offset = buttonHeight;
		#endif
		/*
		GUI.TextArea(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2) + buttonHeight * 0.6f,
			Screen.height*1.8f/3 - (buttonHeight/2),
			buttonWidth,
			buttonHeight*1.2f
			),"Show scores while playing?");
		
		
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + offset,
			buttonWidth,
			buttonHeight
			),"Reset High Scores")) {
			PlayerPrefs.SetFloat("HighScore",0);
		}*/
		stats.transform.position = new Vector3(
			Screen.width*0.5f/3 + stats.preferredWidth/2,
			Screen.height*1.8f/3 - stats.preferredHeight/2,
			0);
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 3 * offset,
			buttonWidth,
			buttonHeight
			),"Back")) {
			gameObject.AddComponent<MenuScript>();
			Destroy(this);
		}
	}
}
