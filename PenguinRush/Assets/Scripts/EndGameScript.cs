using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	void Awake() {
		GetComponentInParent<ObstacleManager>().setFinished(true);
		GetComponentInParent<Score>().stop();
		GetComponentInParent<Score>().saveScore();
	}

	void OnGUI() {
//		const int buttonWidth = 84;
//		const int buttonHeight = 30;
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;

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
