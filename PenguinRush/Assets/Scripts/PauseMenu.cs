using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private Lang lang;
	
	void Start() {
		lang = new Lang(Application.dataPath + "/Languajes/lang.xml", PlayerPrefs.GetString("languaje",Application.systemLanguage.ToString()));
	}

	void OnGUI() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		GUI.skin.button.fontSize = Screen.width/90;
		int buttonWidth = Screen.width/9;
		int buttonHeight = buttonWidth/4;
		int offset = Mathf.FloorToInt(buttonHeight*1.6f);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		GUI.skin.button.fontSize = Screen.width/40;
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		float offset = buttonHeight;
		#endif
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*0.9f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),lang.getString("stage_menu"))) {
			GetComponentInParent<PauseScript>().quitPause();
			Application.LoadLevel("Menu");
		}
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*1.1f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),lang.getString("stage_resume")+"!")) {
			GetComponentInParent<PauseScript>().quitPause();
		}
	}
}
