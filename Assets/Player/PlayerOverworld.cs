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

	void Move(Vector3 dir) {
		Vector3 newPos = transform.position + dir;
		char tile = overworld.GetTile((int)(newPos.x * (1 / scale.x)), (int)(newPos.y * (1 / scale.y)));
		Debug.Log(tile);
		if (tile == 'g') {
			transform.position += dir;
		} else if (tile == 'd') {
			Application.LoadLevel("dungeon");
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
