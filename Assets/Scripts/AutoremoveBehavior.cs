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
		if (player.transform.position.x - transform.position.x > killDistance) {
			Destroy (this.gameObject);
		} else if (transform.position.y < -100.0f) {
			Destroy (this.gameObject);
		}
	}
}
