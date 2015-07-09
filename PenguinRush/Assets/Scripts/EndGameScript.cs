using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	void Awake() {
		GetComponentInParent<ObstacleManager>().setFinished(true);
		GetComponentInParent<Score>().stop();
		GetComponentInParent<Score>().saveScore();
	}

	void OnGUI() {
		const int buttonWidth = 84;
		const int buttonHeight = 30;

		if (GUI.Button(new Rect(
				Screen.width/2 - (buttonWidth/2),
				Screen.height/2 - (buttonHeight*1.1f),
				buttonWidth,
				buttonHeight
				),"Restart")) {
			GetComponentInParent<Score>().resetScore();
			spawnPlayer(); 
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

	void spawnPlayer() {
		GameObject instance = Instantiate(Resources.Load("player"), new Vector3(0,0,0), Quaternion.identity) as GameObject;
		instance.transform.SetParent(GameObject.FindWithTag("Middleground").transform);
		instance.GetComponent<PlayerScript>().setCanDie(false);
		instance.tag = "Player";
		instance.GetComponent<PlayerScript>().scripts = gameObject;
		instance.GetComponent<PolygonCollider2D>().enabled = false;
	}
}
