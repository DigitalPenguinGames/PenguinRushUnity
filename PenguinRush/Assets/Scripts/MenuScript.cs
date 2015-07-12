using UnityEngine;

public class MenuScript : MonoBehaviour {

	void OnGUI() {
		//const int buttonWidth = 120;
		//const int buttonHeight = 30;
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		if (GUI.Button(new Rect(
				Screen.width*0.5f/3 - (buttonWidth/2),
				Screen.height*1.8f/3 - (buttonHeight/2),
				buttonWidth,
				buttonHeight
			),"Start!")) {
			Application.LoadLevel("Stage1");
		}
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + buttonHeight,
			buttonWidth,
			buttonHeight
			),"Reset High Scores!")) {
			PlayerPrefs.SetFloat("HighScore",0);;
		}
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + buttonHeight + buttonHeight,
			buttonWidth,
			buttonHeight
			),"Exit!")) {
			Application.Quit();
		}
	}
}
