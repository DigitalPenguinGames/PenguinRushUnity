using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	

	private bool paused = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			if (!paused) {
				gameObject.AddComponent<PauseMenu>();
				paused = true;
				Time.timeScale = 0;
			}
			else quitPause();
		}
	}

	public void quitPause() {
		if (GetComponentInParent<PauseMenu>() != null) Destroy(GetComponentInParent<PauseMenu>());
		Time.timeScale = 1;
		paused = false;
	}	
}
