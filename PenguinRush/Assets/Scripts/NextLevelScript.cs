using UnityEngine;
using System.Collections;

public class NextLevelScript : MonoBehaviour {

	public string nextScene;

	public void nextLevel() {
		Application.LoadLevel(nextScene);
	}
}
