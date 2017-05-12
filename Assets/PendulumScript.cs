using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumScript : MonoBehaviour {
	ConfigurableJoint joint;
	// Use this for initialization
	void Start () {
		joint = GetComponent<ConfigurableJoint> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Rigidbody b in GetComponentsInChildren<Rigidbody> ()) {
			b.WakeUp ();
		}
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Hands")) {
			GetComponent<Hv_SurfOfLifeSimpleEngine_AudioLib>().SendEvent(Hv_SurfOfLifeSimpleEngine_AudioLib.Event.Action);
		}
	}
}
