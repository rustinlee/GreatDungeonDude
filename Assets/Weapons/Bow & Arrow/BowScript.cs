using UnityEngine;
using System.Collections;

public class BowScript : MonoBehaviour {
	public Transform projectile;
	public float projectileSpeed = 1f;
	public float maxDrawTime = 2f;
	public float reloadTime = 0.5f;

	private Transform arrow;
	private float timeDrawn;
	private float reloadCounter;

	void SpawnArrow() {
		arrow = Instantiate(projectile) as Transform;
		arrow.parent = gameObject.transform;
		arrow.localPosition = new Vector3(0.1f, 0f, 0f);
		arrow.localEulerAngles = projectile.localEulerAngles;
	}

	void Start() {
		SpawnArrow();
	}
	
	void Update() {
		if (arrow != null) {
			if (Input.GetButton("Fire1")) {
				if (timeDrawn <= maxDrawTime) {
					timeDrawn += Time.deltaTime;
					arrow.localPosition -= new Vector3(Time.deltaTime * 0.1f, 0f, 0f);
				}
			} else {
				if (timeDrawn > maxDrawTime / 5) {
					Vector2 newVel = Quaternion.AngleAxis(arrow.rotation.eulerAngles.z + 90, Vector3.forward) * new Vector2(5f * projectileSpeed * timeDrawn, 0f);
					arrow.rigidbody2D.velocity += newVel;
					arrow.GetComponent<ArrowScript>().SetFlying();
					arrow.parent = null;
					arrow = null;
					reloadCounter = 0;
				}

				timeDrawn = 0;
			}
		} else {
			reloadCounter += Time.deltaTime;
			if (reloadCounter >= reloadTime) {
				SpawnArrow();
			}
		}

	}
}
