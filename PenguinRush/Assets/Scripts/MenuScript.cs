using UnityEngine;

public class MenuScript : MonoBehaviour {

	void OnGUI() {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
		const int buttonWidth = 120;
		const int buttonHeight = 30;
		const int offset = buttonHeight + 20;
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		const float buttonHeight = Screen.height * 1/10;
		const float buttonWidth = buttonHeight * 5;
		const float RectOffset = buttonHeight;
#endif
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
			Screen.height*1.8f/3 - (buttonHeight/2) + offset,
			buttonWidth,
			buttonHeight
			),"Reset High Scores!")) {
			PlayerPrefs.SetFloat("HighScore",0);;
		}
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 2 * offset,
			buttonWidth,
			buttonHeight
			),"Exit!")) {
			Application.Quit();
		}
	}
}
