using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
		
	private Vector3 offset;
	private GameObject player;

	void Start () {
		player = GameObject.FindWithTag("Player");

		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
