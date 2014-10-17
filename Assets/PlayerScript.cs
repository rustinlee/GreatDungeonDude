using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public float speed = 2.0f;

	private Vector3 mousePos;
	private Vector3 velocity = new Vector3(0f, 0f, 0f);

	void Start() {

	}
	
	void Update() {
		mousePos = Input.mousePosition;
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		velocity.x = Input.GetAxis("Horizontal") * speed;
		velocity.y = Input.GetAxis("Vertical") * speed;
	}

	void FixedUpdate() {
		transform.position += velocity;
	}
}
