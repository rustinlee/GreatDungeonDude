using UnityEngine;
using System.Collections;

public class PlayerOverworld : MonoBehaviour {
	public float moveCooldown;

	private OverworldGenerator overworld;
	private float timeSinceMoved = 0f;
	private Vector2 scale;

	void Start() {
		overworld = GameObject.Find("Overworld").GetComponent<OverworldGenerator>();
		scale = overworld.tileSize;
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.W)) {
			transform.position += Vector3.up * scale.y;
			transform.localEulerAngles = new Vector3(0, 0, 90);
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			transform.position += Vector3.left * scale.x;
			transform.localEulerAngles = new Vector3(0, 0, 180);
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			transform.position += Vector3.down * scale.x;
			transform.localEulerAngles = new Vector3(0, 0, 270);
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			transform.position += Vector3.right * scale.y;
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}
}
