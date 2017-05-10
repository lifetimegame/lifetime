using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	public float movementSpeed = 10.0f;

//	private Rigidbody rb;

	void Start () {
//		rb = GetComponent<Rigidbody> ();
//		Vector3 vel = rb.velocity;
//		vel.x = movementSpeed;
//		rb.velocity = vel;
	}

	void Update () {
		float f = 10.0f;
//		Vector3 vel = rb.velocity;
		Vector3 forward = new Vector3(1.0f*movementSpeed, 0.0f, 0.0f);
		GetComponent<CharacterController>().SimpleMove(forward);
//		Vector3 vel = rb.velocity;
//		vel.x = movementSpeed;
//		rb.velocity = vel;
	}
}
