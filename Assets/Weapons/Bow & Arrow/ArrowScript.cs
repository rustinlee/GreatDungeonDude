using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	public float TTL = 5f;
	public int damage = 5;

	private float timeAlive = 0f;
	private bool isFlying = false;

	void Start() {
	
	}

	void Update() {
		if (isFlying) {
			timeAlive += Time.deltaTime;

			if (timeAlive >= TTL) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			Destroy(rigidbody2D);
			transform.parent = coll.transform;
			coll.gameObject.GetComponent<EnemyScript>().DamageHP(damage);
		}
	}

	public void SetFlying() {
		isFlying = true;
	}
}
