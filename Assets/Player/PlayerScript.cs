using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	public float speed = 2.0f;
	public List<Transform> weapons;
	public int maxHP = 100;

	private Vector3 mousePos;
	private Vector3 velocity = new Vector3(0f, 0f, 0f);
	//private Animator animator;
	private Transform weapon;
	private int wpnIndex = 0;
	private Text goldCounter;
	private Slider healthBarSlider;
	private float HP;
	private float speedMod;
	private float ladderTimer;
	private Camera renderCam;
	public GameObject deathUI;

	public void Restart() {
		ApplicationModel.ResetGame();
		Application.LoadLevel("overworld");
	}

	public void DamageHP(int amt) {
		HP -= amt;
		
		if (HP <= 0) {
			Time.timeScale = 0;
			deathUI.SetActive(true);
		}

		healthBarSlider.value = HP / maxHP;
	}

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
		renderCam = GameObject.FindGameObjectWithTag("RenderCamera").GetComponent<Camera>();
		deathUI = GameObject.FindGameObjectWithTag("DeathPopup");
		deathUI.active = false;

		//animator = gameObject.GetComponent<Animator>();
		gameObject.GetComponent<SpriteRenderer>().sprite = ApplicationModel.playerSprite;
		healthBarSlider = GameObject.Find("HPSlider").GetComponent<Slider>();
		goldCounter = GameObject.Find("GoldCounter").GetComponent<Text>();

		HP = maxHP;
		EquipWeapon(wpnIndex);
	}
	
	void Update() {
		//rotation
		mousePos = Input.mousePosition;
		Vector3 objectPos = renderCam.WorldToScreenPoint(transform.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = Vector2.ClampMagnitude(input, 1);

		//movement
		speedMod = 1 + 0.03f * ApplicationModel.playerAgi;
		velocity.x = input.x * speed * speedMod;
		velocity.y = input.y * speed * speedMod;

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
			ApplicationModel.playerGold += coll.gameObject.GetComponent<GoldScript>().goldValue;
			goldCounter.text = ApplicationModel.playerGold.ToString();
			Destroy(coll.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.tag == "Ladder") {
			ladderTimer += Time.deltaTime;
			if (ladderTimer > 1)
				Application.LoadLevel("overworld");
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.tag == "Ladder") {
			ladderTimer = 0;
		}
	}
}
