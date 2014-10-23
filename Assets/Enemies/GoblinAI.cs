using UnityEngine;
using System.Collections;

public class GoblinAI : MonoBehaviour {
	public Transform projectile;
	public float projectileVelocity = 1f;
	public float baseMoveSpeed = 1f;
	public float chargingMoveSpeedModifier = 0.5f;
	public float chargeTime = 0.5f;
	public float reloadTime = 1f;
	public float aggroMinDist;
	public float aggroMaxDist;
	
	private Transform spear;
	private Transform target;
	private float moveSpeed;
	private bool throwingSpear = false;
	private float timeCharging = 0f;
	private float timeSinceThrown = 0f;

	void SpawnSpear() {
		spear = Instantiate(projectile) as Transform;
		spear.parent = transform;
		spear.transform.localPosition = projectile.transform.position;
		spear.localEulerAngles = projectile.localEulerAngles;
	}

	void Start() {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		moveSpeed = baseMoveSpeed;
		//SpawnSpear();
	}

	void Update() {
		if (!throwingSpear) {
			timeSinceThrown += Time.deltaTime;
		} else {
			moveSpeed = baseMoveSpeed * chargingMoveSpeedModifier;
		}

		float dist = Vector2.Distance(transform.position, target.position);

		if (dist > aggroMaxDist) {
			rigidbody2D.AddForce(transform.right * moveSpeed);
		} else if (dist < aggroMinDist) {
			rigidbody2D.AddForce(transform.right * -moveSpeed);
		} else {
			if (timeSinceThrown > reloadTime && spear == null) {
				SpawnSpear();
				timeSinceThrown = 0f;
			}

			if (!throwingSpear && spear != null) {
				throwingSpear = true;
			}

			if (throwingSpear) {
				timeCharging += Time.deltaTime;
				Vector3 dir = target.position - spear.position;
				float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				spear.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				if (timeCharging > chargeTime) {
					spear.parent = null;
					spear.rigidbody2D.isKinematic = false;
					spear.rigidbody2D.AddForce(spear.right * projectileVelocity * 5f);
					spear.GetComponent<ArrowScript>().SetFlying();
					spear = null;
					throwingSpear = false;
					timeCharging = 0f;
				}
			}
		}
	}
}
