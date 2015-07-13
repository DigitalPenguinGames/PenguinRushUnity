using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	void OnGUI() {
//		const int buttonWidth = 83;
//		const int buttonHeight = 30;
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*0.8f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),"Menu!")) {
			GetComponentInParent<PauseScript>().quitPause();
			Application.LoadLevel("Menu");
		}
		
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height*1.2f/2 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),"Resume!")) {
			GetComponentInParent<PauseScript>().quitPause();
		}
	}
}
