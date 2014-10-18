using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public float speed = 2.0f;

	private Vector3 mousePos;
	private Vector3 velocity = new Vector3(0f, 0f, 0f);
	private Animator animator;

	void Start() {
		animator = gameObject.GetComponent<Animator>();
	}
	
	void Update() {
		//rotation
		mousePos = Input.mousePosition;
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		//movement
		velocity.x = Input.GetAxis("Horizontal") * speed;
		velocity.y = Input.GetAxis("Vertical") * speed;

		//fire
		if (Input.GetButtonDown("Fire1")) {
			animator.SetTrigger("swingSword");
		}
	}

	void FixedUpdate() {
		transform.position += velocity;
	}
}
