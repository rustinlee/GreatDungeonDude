using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	public float TTL = 5f;
	public int damage = 5;
	public bool friendly = true;

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
		if (coll.gameObject.tag == "Environment" || (friendly && coll.gameObject.tag == "Enemy") || (!friendly && coll.gameObject.tag == "Player")) {
			Destroy(rigidbody2D);
			transform.parent = coll.transform;
			
			if (friendly && coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<EnemyScript>().DamageHP(damage);
			} else if (!friendly && coll.gameObject.tag == "Player") {
				//damage player
			}
		}
	}
	
	public void SetFlying() {
		GetComponent<BoxCollider2D>().enabled = true;
		isFlying = true;
	}
}
