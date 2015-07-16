using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	void Awake() {
		GetComponentInParent<ObstacleManager>().setFinished(true);
		GetComponentInParent<Score>().stop();
		GetComponentInParent<Score>().enableTexFields(true);
	}

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
				Screen.height/2 - (buttonHeight*1.1f),
				buttonWidth,
				buttonHeight
				),"Restart")) {
			GetComponentInParent<Score>().resetScore();
			GetComponentInParent<CountTimerScript>().restart();
		}
		if (GUI.Button(new Rect(
			Screen.width/2 - (buttonWidth/2),
			Screen.height/2 + (buttonHeight*1.1f),
			buttonWidth,
			buttonHeight
			),"Menu")) {
			Application.LoadLevel("Menu");

		}
		
	}


}
