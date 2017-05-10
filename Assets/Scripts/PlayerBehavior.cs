using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	public float movementSpeed = 10.0f;

	public float freq = 5.0f;
	public float amplitude = 100.0f;

	private float totalTime = 0.0f;
	private float targetZ = 0.0f;

	void Start () {
		
	}

	void Update () {
		float distanceX = transform.position.x;
		float distanceZ = transform.position.z;

		targetZ = Mathf.Sin (distanceX / freq) * amplitude;
		Rigidbody rb = GetComponent<Rigidbody>();
		float movementZ = targetZ - transform.position.z;

		Vector3 vel = rb.velocity;
		vel.x = movementSpeed;
		vel.z = movementZ;
		rb.velocity = vel;

		Quaternion rot = transform.rotation;
		rot.x = -(distanceZ / amplitude) * 0.5f;
//		rot.z = -(distanceZ / amplitude);
		transform.rotation = rot;

	}
}
