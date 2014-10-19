using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	public float TTL = 5f;

	private float timeAlive = 0f;
	private bool isFlying = false;

	void Start() {
	
	}

	void Update() {
		if (isFlying) {
			timeAlive += Time.deltaTime;

			if (timeAlive >= TTL) {
				Destroy(gameObject);
			}
		}
	}

	public void SetFlying() {
		isFlying = true;
	}
}
