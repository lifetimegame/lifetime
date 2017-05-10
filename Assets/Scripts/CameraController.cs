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
		Vector3 pos = player.transform.position + offset;
		pos.z = 0.0f;
		
		transform.position = pos;
	}
}
