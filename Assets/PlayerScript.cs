using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	public float speed = 2.0f;
	public List<Transform> weapons;

	private Vector3 mousePos;
	private Vector3 velocity = new Vector3(0f, 0f, 0f);
	private Animator animator;
	private Transform weapon;
	private int wpnIndex = 0;
	private Text goldCounter;

	void EquipWeapon(int index) {
		if (weapon != null) {
			Destroy(weapon.gameObject);
		}

		weapon = Instantiate(weapons[index]) as Transform;
		weapon.parent = gameObject.transform;
		weapon.localPosition = weapons[index].position;
		weapon.localEulerAngles = weapons[index].eulerAngles;
	}

	void Start() {
		animator = gameObject.GetComponent<Animator>();
		goldCounter = GameObject.Find("GoldCounter").GetComponent<Text>();

		EquipWeapon(wpnIndex);
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

		//weapon cycling
		if (Input.GetButtonDown("Cycle1")) {
			wpnIndex++;

			if (wpnIndex > weapons.Count - 1) {
				wpnIndex = 0;
			}

			EquipWeapon(wpnIndex);
		} else if (Input.GetButtonDown("Cycle2")) {
			wpnIndex--;

			if (wpnIndex < 0) {
				wpnIndex = weapons.Count - 1;
			}

			EquipWeapon(wpnIndex);
		}
	}

	void FixedUpdate() {
		transform.position += velocity;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "GoldPickup") {
			int currentGold = Convert.ToInt32(goldCounter.text);
			currentGold += coll.gameObject.GetComponent<GoldScript>().goldValue;
			goldCounter.text = currentGold.ToString();
			Destroy(coll.gameObject);
		}
	}
}
