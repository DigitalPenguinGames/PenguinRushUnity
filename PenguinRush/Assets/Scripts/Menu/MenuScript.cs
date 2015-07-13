using UnityEngine;

public class MenuScript : MonoBehaviour {

	void OnGUI() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		GUI.skin.button.fontSize = Screen.width/90;
		int buttonWidth = Screen.width/9;
		int buttonHeight = buttonWidth/4;
		int offset = Mathf.FloorToInt(buttonHeight*1.6f);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		float offset = buttonHeight;
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
			),"Reset High Scores")) {
			PlayerPrefs.SetFloat("HighScore",0);;
		}
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 2 * offset,
			buttonWidth,
			buttonHeight
			),"Options")) {
			gameObject.AddComponent<OptionsScript>();
			Destroy(this);
		}if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 3 * offset,
			buttonWidth,
			buttonHeight
			),"Exit")) {
			Application.Quit();
		}
	}
}
