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
		float f = 10.0f;
		Vector3 vel = rb.velocity;
		if ((Input.GetKeyDown ("space"))) {
			
		}
		if ((Input.GetKeyDown ("w"))) {
			Debug.Log("Press W");
			vel.x -= f;
		}
		if ((Input.GetKeyDown ("s"))) {
			vel.x += f;
		}
		if ((Input.GetKeyDown ("a"))) {
			rb.AddForce(new Vector3(0, f, 0));
		}
		if ((Input.GetKeyDown ("d"))) {
			rb.AddForce(new Vector3(0, -f, 0));
		}
//		Vector3 vel = rb.velocity;
//		vel.x = movementSpeed;
//		rb.velocity = vel;
	}
}
