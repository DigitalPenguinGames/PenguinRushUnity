using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour {

	//private int numberOfTries;
	private Text stats;	
	private Vector3 textPos;

	// Use this for initialization
	void Start () {
		loadStats();
		textPos = new Vector3(
			Screen.width*0.8f/3,
			Screen.height*1.8f/3 - stats.preferredHeight/2,
			0);
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
		stats.transform.position = textPos;

		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 2 * offset,
			buttonWidth,
			buttonHeight
			),"Reset Stats")) {
			resetStats();
			loadStats();

		}
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

	private void loadStats() {
		int Tries = PlayerPrefs.GetInt("trackTries",0);
		int Jumps = PlayerPrefs.GetInt("trackJumps",0);
		int TotalTime = Mathf.FloorToInt(PlayerPrefs.GetFloat("trackTotalTime",0));
		int HighScore = Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore",0))	;
		int TotalScore = Mathf.FloorToInt(PlayerPrefs.GetFloat("trackTotalScore",0));
		
		stats = GameObject.Find("Stats").GetComponent<Text>();
		stats.text = "Number of \n" +
				"Tries: " + Tries + "\n" +
				"Jumps: " + Jumps + "\n" +
				"Total Time: " + formateTime(TotalTime) + "\n" +
				"High Score: " + HighScore + "\n" + 
				"Total Score: " + TotalScore + "\n"
				;
	}

	private void resetStats() {
		PlayerPrefs.SetInt("trackTries",0);
		PlayerPrefs.SetInt("trackJumps",0);
		PlayerPrefs.SetFloat("trackTotalTime",0);
		PlayerPrefs.SetFloat("HighScore",0);
		PlayerPrefs.SetFloat("trackTotalScore",0);
	}

	private string formateTime(int time) {
		string ret = "";
		int secs = time%60;
		time /= 60;
		int mins = time%60;
		time /= 60;
		int hours = time;
		if (hours < 10) ret = ret + "0";
		ret = ret + hours + ":";
		if (mins < 10) ret = ret + "0";
		ret = ret + mins + ":";
		if (secs < 10) ret = ret + "0";
		ret = ret + secs;
		return ret;
	}
}
