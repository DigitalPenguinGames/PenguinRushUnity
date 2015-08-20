using UnityEngine;

public class MenuScript : MonoBehaviour {

	private Lang lang;

	void Start() {
		lang = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LangManager>().lang;
	}

	void OnGUI() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		GUI.skin.button.fontSize = Screen.width/90;
		int buttonWidth = Screen.width/9;
		int buttonHeight = buttonWidth/4;
		int offset = Mathf.FloorToInt(buttonHeight*1.6f);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		GUI.skin.button.fontSize = Screen.width/40;
		float buttonHeight = Screen.height * 1/10;
		float buttonWidth = buttonHeight * 5;
		float offset = buttonHeight;
		#endif
		if (GUI.Button(new Rect(
				Screen.width*0.5f/3 - (buttonWidth/2),
				Screen.height*1.8f/3 - (buttonHeight/2),
				buttonWidth,
				buttonHeight
			),lang.getString("menu_start")+"!")) {
			Application.LoadLevel("Stage1");
		}
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + offset,
			buttonWidth,
			buttonHeight
			),lang.getString("menu_options"))) {
			gameObject.AddComponent<OptionsScript>();
			Destroy(this);
		}
		if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 2 * offset,
			buttonWidth,
			buttonHeight
			),lang.getString("menu_stats"))) {
			gameObject.AddComponent<StatsScript>();
			Destroy(this);
		}if (GUI.Button(new Rect(
			Screen.width*0.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 3 * offset,
			buttonWidth,
			buttonHeight
			),lang.getString("menu_exit"))) {
			Application.Quit();
		}

		// For testing on the build
		if (GUI.Button(new Rect(
			Screen.width*1.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2),
			buttonWidth,
			buttonHeight
			),"Nivel 1")) {
			Application.LoadLevel("Stage1");
		}
		if (GUI.Button(new Rect(
			Screen.width*1.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + offset,
			buttonWidth,
			buttonHeight
			),"Nivel 2")) {
			Application.LoadLevel("Stage2");
		}
		if (GUI.Button(new Rect(
			Screen.width*1.5f/3 - (buttonWidth/2),
			Screen.height*1.8f/3 - (buttonHeight/2) + 2 * offset,
			buttonWidth,
			buttonHeight
			),"Nivel 3")) {
			Application.LoadLevel("Stage3");
		}
	}
}
