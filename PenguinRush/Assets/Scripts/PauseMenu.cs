using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	void OnGUI() {
		
#if UNITY_STANDALONE || UNITY_WEBPLAYER
		const int buttonWidth = 83;
		const int buttonHeight = 30;
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		const float buttonHeight = Screen.height * 1/10;
		const float buttonWidth = buttonHeight * 5;
#endif
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*0.9f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),"Menu!")) {
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
