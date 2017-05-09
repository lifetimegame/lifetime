using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AutoremoveBehavior : MonoBehaviour {
	public float killDistance = 64.0f;

	private GameObject player;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		
		if (Vector3.Distance(player.transform.position, transform.position) > killDistance) {
			Debug.Log("Destroy obsticle");
			Destroy (this.gameObject);
		}
	}
}
