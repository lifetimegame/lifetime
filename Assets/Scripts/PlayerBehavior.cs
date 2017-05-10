using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	public float movementSpeed = 10.0f;

	public float freq = 5.0f;
	public float amplitude = 3.0f;

	private float totalTime = 0.0f;
	private float targetZ = 0.0f;

	void Start () {
		
	}

	void Update () {
		float distanceX = transform.position.x;

		CharacterController controller = GetComponent<CharacterController> ();
		Vector3 movementX = new Vector3(1.0f * movementSpeed, 0.0f, 0.0f);

		targetZ = Mathf.Sin (distanceX / freq) * amplitude;
		Vector3 movementZ = new Vector3(0.0f, 0.0f, targetZ - transform.position.z);
//		Vector3 movementZ = new Vector3(0.0f, 0.0f, targetZ);
		Vector3 movementTotal = movementX + movementZ;

//		Debug.Log ("X: " + movementX + ", Z: " + movementZ + ", Total: " + movementTotal);

		controller.SimpleMove (movementTotal);


	}
}
