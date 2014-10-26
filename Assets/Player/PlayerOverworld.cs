using UnityEngine;
using System.Collections;

public class PlayerOverworld : MonoBehaviour {
	private OverworldGenerator overworld;
	private Vector2 scale;

	void Start() {
		overworld = GameObject.Find("Overworld").GetComponent<OverworldGenerator>();
		scale = overworld.tileSize;
		transform.localScale = new Vector3(scale.y * 2, scale.y * 2, 1); //intentionally using y for both
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}

	void Move(Vector3 dir) {
		Vector3 newPos = transform.position + dir;
		char tile = overworld.GetTile((int)(newPos.x * (1 / scale.x)), (int)(newPos.y * (1 / scale.y)));
		Debug.Log(tile);
		if (tile == 'g') {
			transform.position += dir;
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
		} else if (tile == 'd') {
			Application.LoadLevel("dungeon");
		} else if (tile == 'p') {
			Application.LoadLevel("store");
		}
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.W)) {
			//transform.position += Vector3.up * scale.y;
			Move(Vector3.up * scale.y);
			transform.localEulerAngles = new Vector3(0, 0, 90);
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			//transform.position += Vector3.left * scale.x;
			Move(Vector3.left * scale.x);
			transform.localEulerAngles = new Vector3(0, 0, 180);
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			//transform.position += Vector3.down * scale.x;
			Move(Vector3.down * scale.x);
			transform.localEulerAngles = new Vector3(0, 0, 270);
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			//transform.position += Vector3.right * scale.y;
			Move(Vector3.right * scale.y);
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}
}
