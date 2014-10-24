using UnityEngine;
using System.Collections;

public class BatAI : MonoBehaviour {
	public float baseMoveSpeed = 1f;
	public float aggroMinDist;
	public float aggroMaxDist;

	private Transform target;
	private float moveSpeed;
	private bool isCharging;
	private float sinSpeed; //speed of sine modifier

	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		moveSpeed = baseMoveSpeed;

		float extraRange = Random.Range(-0.2f, 0.2f); //keeps bats from flying too similarly
		aggroMinDist += extraRange;
		aggroMaxDist += extraRange;

		sinSpeed = Random.Range(2.0f, 3.0f);
	}
	
	void Update () {
		float dist = Vector2.Distance(transform.position, target.position);

		if (dist > aggroMaxDist - (Mathf.Sin(Time.timeSinceLevelLoad * sinSpeed) + 1) * 0.2f) {
			rigidbody2D.AddForce(transform.right * moveSpeed);
		} else if (dist < aggroMinDist - (Mathf.Sin(Time.timeSinceLevelLoad * sinSpeed) + 1) * 0.2f) {
			rigidbody2D.AddForce(transform.right * -moveSpeed);
		}

		//Debug.Log(Mathf.Sin(Time.timeSinceLevelLoad) + 1);
	}
}
