using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {

	public ObstacleSpawner spawner;
	public int spawningPatter = 0;

	private float distance;

	void Start() {
		distance = spawner.speed.x * spawner.time.x;
	}

	public void start() {
		spawner.start();
	}

	public void stop() {
		spawner.stop();
	}

	public void propagateSpeed(float speed) {
		spawner.propagateSpeed(speed);
	}

	public void spawningAnother() {
		float time;
		float speedFactor;
		float timeFactor;
		switch (spawningPatter) {
		case 1:
			// The props are always at the same distance ( faster and sooner)
			speedFactor = spawner.getSpeedFactor();
			speedFactor += 0.02f;
			time = distance / (spawner.speed.x * speedFactor);
			if (time > 100) {
				spawner.setTimeFactor(time/spawner.time.x);	
				spawner.propagateSpeed(speedFactor);
			}
			break;
		case 2:
			// Legacy
			time = spawner.time.x * spawner.getTimeFactor() * 0.985f;
			if (time > 90) spawner.setTimeFactor(time/spawner.time.x);
			break;
		default:
			// First type of spawning 
			spawner.propagateSpeed(spawner.getSpeedFactor() + 0.05f);
			break;
		
		
		}

	}
}
