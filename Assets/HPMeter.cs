using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMeter : MonoBehaviour {
	private GameObject player;
	private PlayerBehavior pb;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		pb = player.GetComponent<PlayerBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;
		scale.x = pb.hp;
		transform.localScale = scale;
	}
}
