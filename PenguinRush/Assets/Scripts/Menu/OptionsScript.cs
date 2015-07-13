using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {

	private bool showScores;

	void Start() {
		showScores = PlayerPrefs.GetInt("showScores",1) == 1;
		print (showScores);
	}

	void OnGUI() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		GUI.skin.button.fontSize = Screen.width/90;
		GUI.skin.textArea.fontSize = Screen.width/90;
		int buttonWidth = Screen.width/9;
		int buttonHeight = buttonWidth/4;
		int offset = Mathf.FloorToInt(buttonHeight*1.6f);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
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
			Screen.width*0.5f/3 - (buttonWidth/2) + offset,
			Screen.height*1.8f/3 - (buttonHeight/2),
			buttonWidth,
			buttonHeight*1.2f
			),"Show scores while playing?");


		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + offset,
			buttonWidth,
			buttonHeight
			),"Start!")) {
			Application.LoadLevel("Stage1");

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
