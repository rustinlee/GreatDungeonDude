using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {
	private Transform player;

	void Awake() {
		Application.targetFrameRate = 120; //throwing this here for now since it's the only camera script
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}
}
