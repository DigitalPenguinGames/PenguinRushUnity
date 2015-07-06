using UnityEngine;

public class MenuScript : MonoBehaviour {

	void OnGUI() {
		const int buttonWidth = 84;
		const int buttonHeight = 30;

		Rect buttonRect = new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
		);

		if (GUI.Button(buttonRect,"Start!")) {
			Application.LoadLevel("Stage1");
		}

	}
}
