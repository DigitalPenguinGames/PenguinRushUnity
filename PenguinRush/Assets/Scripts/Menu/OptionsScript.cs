using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {

	private bool showScores;
	#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
	private bool touchType;
	#endif

	void Start() {
		showScores = PlayerPrefs.GetInt("showScores",1) == 1;
		#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		touchType = PlayerPrefs.GetInt("touchType",1) == 1;
		#endif
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

		showScores = (GUI.Toggle(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2),
			buttonHeight,
			buttonHeight
			),showScores, ""));
		PlayerPrefs.SetInt("showScores",showScores ? 1 : 0);
		GUI.TextArea(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2) + buttonHeight * 0.6f,
			Screen.height*1.8f/3 - (buttonHeight/2),
			buttonWidth,
			buttonHeight*1.2f
			),"Show scores while playing?");
		
		#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		touchType = (GUI.Toggle(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*2.2f/3 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),touchType, "Horizontal"));
		touchType = !(GUI.Toggle(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*2.2f/3 - (buttonHeight/2) + buttonHeight,
			buttonWidth,
			buttonHeight
			),!touchType, "Vertical"));
		PlayerPrefs.SetInt("touchTypeHorizontal",touchType ? 1 : 0);
		
		#endif

		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + offset,
			buttonWidth,
			buttonHeight
			),"Reset High Scores")) {
			PlayerPrefs.SetFloat("HighScore",0);
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
}
