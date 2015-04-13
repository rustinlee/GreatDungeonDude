using UnityEngine;
using System.Collections;

public class BatAI : MonoBehaviour {
	public float baseMoveSpeed = 1f;
	public float chargeChance; //chance per second that bat will charge at target
	public float chargeCooldown; //time spent vulnerable/recovering after charging
	public float chargeForce;
	public float aggroMinDist;
	public float aggroMaxDist;
	public int damage;

	private Transform target;
	private float moveSpeed;
	private bool isCharging;
	private float timeSinceCharge;
	private float sinSpeed; //speed of sine modifier
	private Vector2 vel;
	private BoxCollider2D mouthCollider;

	void StartCharge() {
		vel = Vector2.zero;
		timeSinceCharge = 0;
		mouthCollider.enabled = true;
		GetComponent<Rigidbody2D>().drag *= 0.5f;
		isCharging = true;

		GetComponent<Rigidbody2D>().AddForce(transform.right * chargeForce);
	}

	void StopCharge() {
		mouthCollider.enabled = false;
		GetComponent<Rigidbody2D>().drag *= 2f;
		isCharging = false;
	}

	void Start() {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		moveSpeed = baseMoveSpeed;
		mouthCollider = gameObject.GetComponent<BoxCollider2D>();

		float extraRange = Random.Range(-0.4f, 0.4f); //keeps bats from flying too similarly
		aggroMinDist += extraRange;
		aggroMaxDist += extraRange;

		sinSpeed = Random.Range(2.0f, 4.0f);
	}
	
	void Update() {
		float dist = Vector2.Distance(transform.position, target.position);

		if (!isCharging) {
			if (dist > aggroMaxDist - (Mathf.Sin(Time.timeSinceLevelLoad * sinSpeed) + 1) * 0.2f) {
				vel = transform.right * moveSpeed;
			} else if (dist < aggroMinDist - (Mathf.Sin(Time.timeSinceLevelLoad * sinSpeed) + 1) * 0.2f) {
				vel = transform.right * -moveSpeed;
			} else {
				vel = Vector2.zero;
			}

			if (Random.Range(0f, 1f) < chargeChance * Time.deltaTime) {
				StartCharge();
			}
		} else {
			timeSinceCharge += Time.deltaTime;
			if (timeSinceCharge > chargeCooldown)
				StopCharge();
		}
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity += vel;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PlayerScript>().DamageHP(damage);
			StopCharge();
		}
	}
}
