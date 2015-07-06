using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	void OnGUI() {
		const int buttonWidth = 84;
		const int buttonHeight = 30;

		
		if (GUI.Button(new Rect(
				Screen.width/2 - (buttonWidth/2),
				Screen.height/2 - (buttonHeight*1.1f),
				buttonWidth,
				buttonHeight
				),"Restart")) {
			Application.LoadLevel("Stage1");
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
