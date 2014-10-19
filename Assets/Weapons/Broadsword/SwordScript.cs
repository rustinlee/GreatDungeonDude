using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {
	public int damage = 10;

	private BoxCollider2D hitbox;
	private Animator animator;

	void Start() {
		animator = gameObject.GetComponent<Animator>();

		hitbox = gameObject.GetComponent<BoxCollider2D>();
		DisableHitbox();
	}
	
	void Update() {
		//fire
		if (Input.GetButtonDown("Fire1")) {
			animator.SetTrigger("swingSword");
		}
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
