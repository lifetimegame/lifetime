using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	public float movementSpeed = 1.0f;

	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Vector3 vel = rb.velocity;
		vel.x = movementSpeed;
		rb.velocity = vel;
	}

	void Update () {

	}
}
