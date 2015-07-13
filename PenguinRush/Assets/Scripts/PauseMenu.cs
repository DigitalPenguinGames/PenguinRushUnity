using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	void OnGUI() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		GUI.skin.button.fontSize = Screen.width/90;
		int buttonWidth = Screen.width/9;
		int buttonHeight = buttonWidth/4;
		int offset = Mathf.FloorToInt(buttonHeight*1.6f);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		float RectOffset = buttonHeight;
		#endif
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*0.9f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),"Menu")) {
			GetComponentInParent<PauseScript>().quitPause();
			Application.LoadLevel("Menu");
		}
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*1.1f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),"Resume!")) {
			GetComponentInParent<PauseScript>().quitPause();
		}
	}
}
