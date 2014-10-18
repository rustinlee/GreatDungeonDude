using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {
	public int damage = 10;

	private BoxCollider2D hitbox;

	void Start() {
		hitbox = gameObject.GetComponent<BoxCollider2D>();
		DisableHitbox();
	}
	
	void Update() {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<EnemyScript>().DamageHP(damage);
		}
	}

	void EnableHitbox() {
		hitbox.enabled = true;
	}

	void DisableHitbox() {
		hitbox.enabled = false;
	}
}
