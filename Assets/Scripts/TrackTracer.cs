using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTracer : MonoBehaviour {
	public float distance = 10.0f;
	public float posY = 2.0f;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");

	}

	// Update is called once per frame
	void Update () {
		PlayerBehavior pb = player.GetComponent<PlayerBehavior> ();
		float playerX = player.transform.position.x;

		float posX = playerX + distance;
		float posZ = Mathf.Sin (posX / pb.freq) * pb.amplitude;

		Vector3 pos = transform.position;
		pos.x = posX;
		pos.z = posZ;
		pos.y = posY;
		transform.position = pos;
	}
}
