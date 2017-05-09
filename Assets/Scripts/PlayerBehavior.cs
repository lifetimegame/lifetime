using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	public float movementSpeed = 10.0f;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		Vector3 vel = rb.velocity;
		vel.x = movementSpeed;
		rb.velocity = vel;
	}

	void Update () {
		if ((Input.GetKeyDown ("space"))) {
			rb.AddForce(new Vector3(0, 0, 1));
		}

		Vector3 vel = rb.velocity;
		vel.x = movementSpeed;
		rb.velocity = vel;
	}
}
