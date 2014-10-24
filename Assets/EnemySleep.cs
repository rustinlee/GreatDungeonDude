using UnityEngine;
using System.Collections;

public class EnemySleep : MonoBehaviour {
	public float sleepDistance = 1f;

	void Start () {
	
	}
	
	void Update () {
		foreach (Transform child in this.transform) {
			Vector3 viewPos = Camera.main.WorldToViewportPoint(child.position);
			bool nearViewport = (viewPos.x > 0 - sleepDistance && viewPos.x < 1 + sleepDistance) &&
								(viewPos.y > 0 - sleepDistance && viewPos.y < 1 + sleepDistance);
			child.gameObject.SetActive(nearViewport);
		}
	}
}
