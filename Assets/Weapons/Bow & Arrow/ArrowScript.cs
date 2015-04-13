using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	public float TTL = 5f;
	public int damage = 5;
	public bool friendly = true;

	private float timeAlive = 0f;
	private bool isFlying = false;
	private BoxCollider2D box;

	void Start() {
		box = gameObject.GetComponent<BoxCollider2D>();
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
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(box);
			transform.parent = coll.transform;
			
			if (friendly && coll.gameObject.tag == "Enemy") {
				float damageMod = 1 + 0.06f * ApplicationModel.playerDex;
				float effDamage = damage * damageMod;
				coll.gameObject.GetComponent<EnemyScript>().DamageHP(Mathf.FloorToInt(effDamage));
			} else if (!friendly && coll.gameObject.tag == "Player") {
				coll.gameObject.GetComponent<PlayerScript>().DamageHP(damage);
			}
		}
	}
	
	public void SetFlying() {
		GetComponent<BoxCollider2D>().enabled = true;
		isFlying = true;
	}
}
